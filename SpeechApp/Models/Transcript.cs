namespace SpeechApp.Models;

/// <summary>Konusma tanima sonucunun tek bir kaydi (zaman damgali).</summary>
public sealed record Transcript(DateTime Time, string Text, float Confidence)
{
    public string ToLine() => $"[{Time:yyyy-MM-dd HH:mm:ss}] ({Confidence:P0}) {Text}";
}

/// <summary>Uygulamada o anda aktif olan islem.</summary>
public enum ActivityState
{
    Idle,
    Speaking,
    Paused,
    Listening
}
