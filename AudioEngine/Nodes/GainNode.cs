using HighAudioGen.SDK.Buffers;
using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Nodes;

public sealed class GainNode : AudioNodeBase
{
    private static readonly IReadOnlyList<IAudioPort> s_inputs =
    [
        new AudioPort
        {
            Name = "Input",
            ChannelCount = 2
        }
    ];

    private static readonly IReadOnlyList<IAudioPort> s_outputs =
    [
        new AudioPort
        {
            Name = "Output",
            ChannelCount = 2
        }
    ];

    public float Gain { get; init; } = 1.0f;

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
                "GainNode requires exactly one input buffer.");
        }

        var input = inputs[0];

        if (input.ChannelCount != output.ChannelCount)
        {
            throw new InvalidOperationException(
                "Input and output channel counts do not match.");
        }

        if (input.FrameCount != output.FrameCount)
        {
            throw new InvalidOperationException(
                "Input and output frame counts do not match.");
        }

        for (int channel = 0; channel < output.ChannelCount; channel++)
        {
            var inputChannel = input.GetChannel(channel);
            var outputChannel = output.GetChannel(channel);

            for (int frame = 0; frame < output.FrameCount; frame++)
            {
                outputChannel[frame] = inputChannel[frame] * Gain;
            }
        }
    }
}
