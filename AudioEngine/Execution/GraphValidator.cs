using HighAudioGen.AudioEngine.Graph;

namespace HighAudioGen.AudioEngine.Execution;

public sealed class GraphValidator
{
    public void Validate(AudioGraph graph)
    {
        ArgumentNullException.ThrowIfNull(graph);

        ValidateConnections(graph);
    }

    private static void ValidateConnections(AudioGraph graph)
    {
        foreach (var connection in graph.EnumerateConnections())
        {
            if (!graph.EnumerateNodes().Contains(connection.SourceNode))
            {
                throw new InvalidOperationException(
                    $"Source node '{connection.SourceNode.Id}' is not registered in the graph.");
            }

            if (!graph.EnumerateNodes().Contains(connection.DestinationNode))
            {
                throw new InvalidOperationException(
                    $"Destination node '{connection.DestinationNode.Id}' is not registered in the graph.");
            }

            if (connection.SourcePort.ChannelCount !=
                connection.DestinationPort.ChannelCount)
            {
                throw new InvalidOperationException(
                    $"Channel count mismatch: '{connection.SourceNode.Id}' -> '{connection.DestinationNode.Id}'.");
            }
        }
    }
}
