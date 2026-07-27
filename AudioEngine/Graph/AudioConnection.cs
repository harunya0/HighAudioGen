using HighAudioGen.SDK.Buffers;
using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Graph;

public sealed class AudioConnection
{
    public required IAudioNode SourceNode { get; init; }

    public required IAudioPort SourcePort { get; init; }

    public required IAudioNode DestinationNode { get; init; }

    public required IAudioPort DestinationPort { get; init; }

    public AudioBuffer? Buffer { get; internal set; }
}
