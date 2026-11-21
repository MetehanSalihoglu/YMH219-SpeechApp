using System.Globalization;
using System.Speech.Recognition;
using SpeechApp.Models;

namespace SpeechApp.Services;

/// <summary>
/// Mikrofondan konusma tanima (System.Speech). Tanima motoru veya mikrofon
/// yoksa istisna yerine anlasilir hata mesaji dondurur.
/// </summary>
public sealed class SpeechRecognitionService : IDisposable
{
    private SpeechRecognitionEngine? _engine;

    public bool IsListening { get; private set; }

    /// <summary>Metin taninca tetiklenir (arka plan thread'inden).</summary>
    public event Action<Transcript>? Recognized;

    /// <summary>Dinleme sirasinda hata olursa tetiklenir.</summary>
    public event Action<string>? Failed;

    /// <summary>Kurulu tanima dillerinin adlari (bilgi amacli).</summary>
    public static IReadOnlyList<string> GetInstalledLanguages() =>
        SpeechRecognitionEngine.InstalledRecognizers().Select(r => r.Culture.DisplayName).ToList();

    /// <summary>Dinlemeyi baslatir. Basarisizsa kullaniciya gosterilecek hata metnini dondurur.</summary>
    public string? Start()
    {
        if (IsListening) return null;

        try
        {
            var recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            if (recognizers.Count == 0)
            {
                return "Bilgisayarda yüklü bir konuşma tanıma dili bulunamadı.\n"
                     + "Windows Ayarlar > Zaman ve Dil > Konuşma bölümünden bir dil paketi ekleyin.";
            }

            // Once arayuz diline uyan tanima motorunu sec, yoksa ilkini kullan.
            RecognizerInfo info = recognizers.FirstOrDefault(r =>
                    r.Culture.TwoLetterISOLanguageName == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                ?? recognizers[0];

            _engine = new SpeechRecognitionEngine(info);
            _engine.LoadGrammar(new DictationGrammar());
            _engine.SetInputToDefaultAudioDevice();
            _engine.SpeechRecognized += Engine_SpeechRecognized;
            _engine.RecognizeCompleted += Engine_RecognizeCompleted;
            _engine.RecognizeAsync(RecognizeMode.Multiple);
            IsListening = true;
            return null;
        }
        catch (Exception ex) when (ex is InvalidOperationException or ArgumentException
                                       or System.Runtime.InteropServices.COMException or IOException
                                       or UnauthorizedAccessException)
        {
            Cleanup();
            return "Mikrofon veya konuşma tanıma servisine erişilemedi.\n"
                 + "Mikrofonun bağlı ve Windows gizlilik ayarlarında izinli olduğundan emin olun.\n\nAyrıntı: " + ex.Message;
        }
    }

    public void Stop()
    {
        if (!IsListening) return;
        try { _engine?.RecognizeAsyncCancel(); }
        catch (InvalidOperationException) { }
        Cleanup();
    }

    private void Engine_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
    {
        if (e.Result.Confidence < 0.3f || string.IsNullOrWhiteSpace(e.Result.Text)) return;
        Recognized?.Invoke(new Transcript(DateTime.Now, e.Result.Text, e.Result.Confidence));
    }

    private void Engine_RecognizeCompleted(object? sender, RecognizeCompletedEventArgs e)
    {
        if (e.Error is not null && IsListening)
        {
            Cleanup();
            Failed?.Invoke("Dinleme sırasında hata oluştu: " + e.Error.Message);
        }
    }

    private void Cleanup()
    {
        IsListening = false;
        if (_engine is null) return;
        _engine.SpeechRecognized -= Engine_SpeechRecognized;
        _engine.RecognizeCompleted -= Engine_RecognizeCompleted;
        _engine.Dispose();
        _engine = null;
    }

    public void Dispose() => Cleanup();
}
