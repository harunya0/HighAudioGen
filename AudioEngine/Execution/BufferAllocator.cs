using HighAudioGen.AudioEngine.Graph;
using HighAudioGen.SDK.Buffers;
using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Execution;

public sealed class BufferAllocator
{
    public void Allocate(
        AudioGraph graph,
        AudioProcessContext context)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(context);

        foreach (var connection in graph.EnumerateConnections())
        {
            connection.Buffer ??= new AudioBuffer(
                connection.SourcePort.ChannelCount,
                context.FrameCount);
        }
    }
}
