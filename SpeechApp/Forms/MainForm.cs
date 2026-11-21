using System.Text;
using SpeechApp.Models;
using SpeechApp.Services;

namespace SpeechApp.Forms;

/// <summary>
/// Ana pencere: sol tarafta metni okutma (TTS), sag tarafta mikrofondan
/// konusma tanima (STT). Ust seritte aktif islem gosterilir.
/// </summary>
public partial class MainForm : Form
{
    private readonly TextToSpeechService _tts = new();
    private readonly SpeechRecognitionService _stt = new();
    private readonly List<Transcript> _history = new();

    public MainForm()
    {
        InitializeComponent();

        _tts.StateChanged += () => UiInvoke(UpdateActivity);
        _stt.Recognized += t => UiInvoke(() => AddTranscript(t));
        _stt.Failed += msg => UiInvoke(() =>
        {
            UpdateActivity();
            ShowError("Konuşma tanıma", msg);
        });
    }

    private void UiInvoke(Action action)
    {
        if (IsDisposed || !IsHandleCreated) return;
        try { BeginInvoke(action); }
        catch (InvalidOperationException) { /* form kapanirken */ }
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        if (!_tts.IsAvailable)
        {
            ShowError("Metin okuma", _tts.InitError ?? "Metin okuma motoru kullanılamıyor.");
            btnSpeak.Enabled = false;
        }
        else
        {
            foreach (string name in _tts.GetVoiceNames()) cmbVoice.Items.Add(name);
            if (cmbVoice.Items.Count > 0) cmbVoice.SelectedIndex = 0;
        }

        int langCount = 0;
        try { langCount = SpeechRecognitionService.GetInstalledLanguages().Count; }
        catch (Exception ex) when (ex is InvalidOperationException or PlatformNotSupportedException or System.Runtime.InteropServices.COMException) { }

        lblStatus.Text = langCount == 0
            ? "Uyarı: Yüklü konuşma tanıma dili yok; 'Dinle' düğmesi hata mesajı verecektir."
            : $"Hazır. Yüklü ses: {cmbVoice.Items.Count}, tanıma dili: {langCount}.";
        UpdateActivity();
    }

    #region Metin okuma

    private void btnSpeak_Click(object? sender, EventArgs e)
    {
        if (_stt.IsListening)
        {
            lblStatus.Text = "Önce dinlemeyi durdurun (mikrofon ve hoparlör aynı anda kullanılmaz).";
            return;
        }

        string? error = _tts.Speak(rtbTts.Text);
        if (error is not null) ShowError("Metin okuma", error);
        UpdateActivity();
    }

    private void btnPauseTts_Click(object? sender, EventArgs e)
    {
        if (_tts.IsPaused) _tts.Resume();
        else _tts.Pause();
        UpdateActivity();
    }

    private void btnStopTts_Click(object? sender, EventArgs e)
    {
        _tts.Stop();
        UpdateActivity();
    }

    private void btnClearTts_Click(object? sender, EventArgs e)
    {
        _tts.Stop();
        rtbTts.Clear();
        rtbTts.Focus();
        UpdateActivity();
    }

    private void cmbVoice_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbVoice.SelectedItem is string name) _tts.SelectVoice(name);
    }

    private void trackRate_Scroll(object? sender, EventArgs e)
    {
        _tts.SetRate(trackRate.Value);
        lblRate.Text = $"Hız: {trackRate.Value}";
    }

    private void trackVolume_Scroll(object? sender, EventArgs e)
    {
        _tts.SetVolume(trackVolume.Value);
        lblVolume.Text = $"Ses düzeyi: {trackVolume.Value}";
    }

    #endregion

    #region Konusma tanima

    private void btnListen_Click(object? sender, EventArgs e)
    {
        _tts.Stop();
        string? error = _stt.Start();
        if (error is not null)
        {
            ShowError("Konuşma tanıma", error);
        }
        UpdateActivity();
    }

    private void btnStopListen_Click(object? sender, EventArgs e)
    {
        _stt.Stop();
        UpdateActivity();
    }

    private void btnSaveStt_Click(object? sender, EventArgs e)
    {
        if (_history.Count == 0 && string.IsNullOrWhiteSpace(rtbStt.Text))
        {
            MessageBox.Show(this, "Kaydedilecek metin yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        saveDialog.FileName = $"konusma_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
        if (saveDialog.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            var sb = new StringBuilder();
            sb.AppendLine(rtbStt.Text.Trim());
            if (_history.Count > 0)
            {
                sb.AppendLine().AppendLine("--- Zaman damgalı kayıtlar ---");
                foreach (Transcript t in _history) sb.AppendLine(t.ToLine());
            }
            File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);
            lblStatus.Text = "Metin kaydedildi: " + saveDialog.FileName;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            ShowError("Dosya kaydı", "Dosya kaydedilemedi: " + ex.Message);
        }
    }

    private void btnClearStt_Click(object? sender, EventArgs e)
    {
        rtbStt.Clear();
        lvHistory.Items.Clear();
        _history.Clear();
        lblStatus.Text = "Tanıma sonuçları temizlendi.";
    }

    private void AddTranscript(Transcript t)
    {
        _history.Add(t);
        rtbStt.AppendText((rtbStt.TextLength > 0 ? " " : "") + t.Text);
        lvHistory.Items.Add(new ListViewItem(new[] { t.Time.ToString("HH:mm:ss"), t.Text, t.Confidence.ToString("P0") }));
        lvHistory.EnsureVisible(lvHistory.Items.Count - 1);
    }

    #endregion

    /// <summary>Ust seritte aktif islemi gosterir ve dugmeleri duruma gore ayarlar.</summary>
    private void UpdateActivity()
    {
        ActivityState state = _stt.IsListening ? ActivityState.Listening
            : _tts.IsPaused ? ActivityState.Paused
            : _tts.IsSpeaking ? ActivityState.Speaking
            : ActivityState.Idle;

        (lblActivity.Text, lblActivity.BackColor) = state switch
        {
            ActivityState.Speaking => ("🔊 AKTİF İŞLEM: Metin okunuyor...", Color.FromArgb(30, 120, 200)),
            ActivityState.Paused => ("⏸ AKTİF İŞLEM: Okuma duraklatıldı", Color.DarkOrange),
            ActivityState.Listening => ("🎤 AKTİF İŞLEM: Dinleniyor, konuşabilirsiniz...", Color.FromArgb(207, 34, 46)),
            _ => ("● AKTİF İŞLEM: Yok (Boşta)", Color.Gray)
        };

        btnSpeak.Enabled = _tts.IsAvailable && state != ActivityState.Listening;
        btnPauseTts.Enabled = state is ActivityState.Speaking or ActivityState.Paused;
        btnPauseTts.Text = state == ActivityState.Paused ? "▶ Devam" : "⏸ Duraklat";
        btnStopTts.Enabled = btnPauseTts.Enabled;
        btnListen.Enabled = !_stt.IsListening;
        btnStopListen.Enabled = _stt.IsListening;
    }

    private void ShowError(string title, string message)
    {
        lblStatus.Text = message.Split('\n')[0];
        MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        _stt.Dispose();
        _tts.Dispose();
    }
}
