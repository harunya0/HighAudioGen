namespace HighAudioGen.SDK.Graph;

public sealed class AudioProcessContext
{
    public required long PlayheadSample { get; init; }
    public required int SampleRate { get; init; }
    public required int FrameCount { get; init; }
}
