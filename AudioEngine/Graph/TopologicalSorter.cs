using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Graph;

public static class TopologicalSorter
{
    public static IReadOnlyList<IAudioNode> Sort(AudioGraph graph)
    {
        ArgumentNullException.ThrowIfNull(graph);

        var inDegree = graph
            .EnumerateNodes()
            .ToDictionary(node => node, _ => 0);

        foreach (var connection in graph.EnumerateConnections())
        {
            inDegree[connection.DestinationNode]++;
        }

        var queue = new Queue<IAudioNode>(
            inDegree
                .Where(pair => pair.Value == 0)
                .Select(pair => pair.Key));

        var result = new List<IAudioNode>();

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            result.Add(node);

            foreach (var connection in graph.EnumerateOutputs(node))
            {
                inDegree[connection.DestinationNode]--;

                if (inDegree[connection.DestinationNode] == 0)
                {
                    queue.Enqueue(connection.DestinationNode);
                }
            }
        }

        if (result.Count != inDegree.Count)
        {
            throw new InvalidOperationException(
                "The audio graph contains a cycle.");
        }

        return result;
    }
}
