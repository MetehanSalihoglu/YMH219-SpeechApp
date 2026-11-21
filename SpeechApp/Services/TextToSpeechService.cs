using System.Speech.Synthesis;

namespace SpeechApp.Services;

/// <summary>
/// System.Speech.SpeechSynthesizer sarmalayicisi: okuma, duraklatma, durdurma,
/// ses ve hiz secimi. Kaynaklar IDisposable ile serbest birakilir.
/// </summary>
public sealed class TextToSpeechService : IDisposable
{
    private SpeechSynthesizer? _synth;

    /// <summary>Okuma durumu degistiginde tetiklenir (arka plan thread'inden olabilir).</summary>
    public event Action? StateChanged;

    public bool IsSpeaking => _synth?.State == SynthesizerState.Speaking;

    public bool IsPaused => _synth?.State == SynthesizerState.Paused;

    /// <summary>Sentezleyici olusturulamadiysa (ses motoru yok) hata mesaji.</summary>
    public string? InitError { get; private set; }

    public TextToSpeechService()
    {
        try
        {
            _synth = new SpeechSynthesizer();
            _synth.SetOutputToDefaultAudioDevice();
            _synth.SpeakCompleted += (_, _) => StateChanged?.Invoke();
            _synth.StateChanged += (_, _) => StateChanged?.Invoke();
        }
        catch (Exception ex) when (ex is InvalidOperationException or PlatformNotSupportedException or System.Runtime.InteropServices.COMException)
        {
            InitError = "Metin okuma motoru başlatılamadı: " + ex.Message;
            _synth?.Dispose();
            _synth = null;
        }
    }

    public bool IsAvailable => _synth is not null;

    /// <summary>Yuklu seslerin adlari.</summary>
    public IReadOnlyList<string> GetVoiceNames()
    {
        if (_synth is null) return Array.Empty<string>();
        return _synth.GetInstalledVoices().Where(v => v.Enabled).Select(v => v.VoiceInfo.Name).ToList();
    }

    public void SelectVoice(string name)
    {
        try { _synth?.SelectVoice(name); }
        catch (ArgumentException) { /* ses bulunamadi, varsayilan kalir */ }
    }

    /// <summary>Hiz: -10 (yavas) ile 10 (hizli) arasinda.</summary>
    public void SetRate(int rate)
    {
        if (_synth is not null) _synth.Rate = Math.Clamp(rate, -10, 10);
    }

    /// <summary>Ses seviyesi: 0-100.</summary>
    public void SetVolume(int volume)
    {
        if (_synth is not null) _synth.Volume = Math.Clamp(volume, 0, 100);
    }

    /// <summary>Metni asenkron okumaya baslar. Hata olursa mesaj dondurur.</summary>
    public string? Speak(string text)
    {
        if (_synth is null) return InitError ?? "Metin okuma motoru kullanılamıyor.";
        if (string.IsNullOrWhiteSpace(text)) return "Okunacak metin boş.";

        try
        {
            _synth.SpeakAsyncCancelAll();
            _synth.SpeakAsync(text);
            return null;
        }
        catch (Exception ex) when (ex is InvalidOperationException or System.Runtime.InteropServices.COMException)
        {
            return "Metin okunamadı: " + ex.Message;
        }
    }

    public void Pause()
    {
        if (IsSpeaking) _synth!.Pause();
    }

    public void Resume()
    {
        if (IsPaused) _synth!.Resume();
    }

    public void Stop()
    {
        if (_synth is null) return;
        if (IsPaused) _synth.Resume();
        _synth.SpeakAsyncCancelAll();
    }

    public void Dispose()
    {
        _synth?.Dispose();
        _synth = null;
    }
}
