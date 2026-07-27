namespace HighAudioGen.SDK.Graph;

public interface IAudioPort
{
    string Name { get; }
    int ChannelCount { get; }
}
