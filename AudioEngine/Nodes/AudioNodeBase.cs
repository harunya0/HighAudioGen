using HighAudioGen.SDK.Buffers;
using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Nodes;

public abstract class AudioNodeBase : IAudioNode
{
    public required string Id { get; init; }

    public abstract IReadOnlyList<IAudioPort> Inputs { get; }

    public abstract IReadOnlyList<IAudioPort> Outputs { get; }

    public abstract void Process(
        AudioProcessContext context,
        IReadOnlyList<AudioBuffer> inputs,
        AudioBuffer output);
}
