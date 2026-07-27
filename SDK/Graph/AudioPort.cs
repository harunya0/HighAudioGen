namespace HighAudioGen.SDK.Graph;

public sealed record AudioPort : IAudioPort
{
    public required string Name { get; init; }
    public required int ChannelCount { get; init; }
}
