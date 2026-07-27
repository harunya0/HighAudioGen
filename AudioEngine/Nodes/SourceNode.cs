using HighAudioGen.Core.Audio;
using HighAudioGen.SDK.Buffers;
using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Nodes;

public sealed class SourceNode : AudioNodeBase
{
    private static readonly IReadOnlyList<IAudioPort> s_inputs = [];

    private static readonly IReadOnlyList<IAudioPort> s_outputs =
    [
        new AudioPort
        {
            Name = "Output",
            ChannelCount = 2
        }
    ];

    public required WaveData WaveData { get; init; }

    public long StartSample { get; init; }

    public override IReadOnlyList<IAudioPort> Inputs => s_inputs;

    public override IReadOnlyList<IAudioPort> Outputs => s_outputs;

    public override void Process(
        AudioProcessContext context,
        IReadOnlyList<AudioBuffer> inputs,
        AudioBuffer output)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(output);

        if (WaveData.Channels != output.ChannelCount)
        {
            throw new InvalidOperationException(
                "WaveData channel count does not match output buffer.");
        }

        output.Clear();

        int channels = WaveData.Channels;
        long startSample = StartSample + context.PlayheadSample;

        for (int frame = 0; frame < output.FrameCount; frame++)
        {
            long sampleIndex = startSample + frame;

            for (int channel = 0; channel < channels; channel++)
            {
                int sourceIndex = (int)(sampleIndex * channels + channel);

                if ((uint)sourceIndex >= (uint)WaveData.Samples.Length)
                {
                    continue;
                }

                output.GetChannel(channel)[frame] =
                    WaveData.Samples[sourceIndex];
            }
        }
    }
}
