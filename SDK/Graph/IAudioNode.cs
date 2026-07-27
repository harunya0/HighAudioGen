using HighAudioGen.SDK.Buffers;

namespace HighAudioGen.SDK.Graph;

public interface IAudioNode
{
    string Id { get; }

    IReadOnlyList<IAudioPort> Inputs { get; }

    IReadOnlyList<IAudioPort> Outputs { get; }

    void Process(
        AudioProcessContext context,
        IReadOnlyList<AudioBuffer> inputs,
        AudioBuffer output);
}
