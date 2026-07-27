using HighAudioGen.SDK.Buffers;
using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Nodes;

public sealed class OutputNode : AudioNodeBase
{
    private static readonly IReadOnlyList<IAudioPort> s_inputs =
    [
        new AudioPort
        {
            Name = "Input",
            ChannelCount = 2
        }
    ];

    private static readonly IReadOnlyList<IAudioPort> s_outputs = [];

    public override IReadOnlyList<IAudioPort> Inputs => s_inputs;

    public override IReadOnlyList<IAudioPort> Outputs => s_outputs;

    public override void Process(
        AudioProcessContext context,
        IReadOnlyList<AudioBuffer> inputs,
        AudioBuffer output)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(inputs);
        ArgumentNullException.ThrowIfNull(output);

        if (inputs.Count != 1)
        {
            throw new InvalidOperationException(
                "OutputNode requires exactly one input buffer.");
        }

        // 現時点では終端ノードなので何もしない。
    }
}
