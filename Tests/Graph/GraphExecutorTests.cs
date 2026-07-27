using HighAudioGen.AudioEngine.Execution;
using HighAudioGen.AudioEngine.Graph;
using HighAudioGen.AudioEngine.Nodes;
using HighAudioGen.Core.Audio;
using HighAudioGen.SDK.Graph;

using Xunit;

namespace HighAudioGen.Tests.Graph;

public sealed class GraphExecutorTests
{
    [Fact]
    public void Render_Should_Run_Graph_Without_Exception()
    {
        // Arrange
        var source = new SourceNode
        {
            Id = "source",
            WaveData = new WaveData
            {
                SampleRate = 48_000,
                Channels = 2,
                Samples =
                [
                    1.0f, 1.0f,
                    0.5f, 0.5f,
                    0.25f, 0.25f,
                    0.125f, 0.125f
                ]
            }
        };

        var gain = new GainNode
        {
            Id = "gain",
            Gain = 0.5f
        };

        var output = new OutputNode
        {
            Id = "output"
        };

        var graph = new AudioGraph();

        graph.AddNode(source);
        graph.AddNode(gain);
        graph.AddNode(output);

        graph.Connect(source, gain);
        graph.Connect(gain, output);

        var context = new AudioProcessContext
        {
            SampleRate = 48_000,
            FrameCount = 4,
            PlayheadSample = 0
        };

        var engine = new AudioEngine.Execution.AudioEngine();

        // Act
        var exception = Record.Exception(() =>
            engine.Render(graph, context));

        // Assert
        Assert.Null(exception);

        var sourceOutputs = graph.GetOutputConnections(source);
        Assert.Single(sourceOutputs);
        Assert.NotNull(sourceOutputs[0].Buffer);

        var gainOutputs = graph.GetOutputConnections(gain);
        Assert.Single(gainOutputs);
        Assert.NotNull(gainOutputs[0].Buffer);
    }
}
