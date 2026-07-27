using HighAudioGen.AudioEngine.Nodes;
using HighAudioGen.SDK.Buffers;
using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Graph;

public sealed class GraphExecutor
{
    public void Execute(
        AudioGraph graph,
        AudioProcessContext context)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(context);

        foreach (var node in TopologicalSorter.Sort(graph))
        {
            var inputs = graph
                .EnumerateInputs(node)
                .Select(connection => connection.Buffer)
                .Where(buffer => buffer is not null)
                .Cast<AudioBuffer>()
                .ToArray();

            // OutputNode は終端なので Process しない
            if (node is OutputNode)
            {
                continue;
            }

            var output = graph
                .EnumerateOutputs(node)
                .Select(connection => connection.Buffer)
                .FirstOrDefault();

            if (output is null)
            {
                throw new InvalidOperationException(
                    $"Node '{node.Id}' has no output buffer.");
            }

            node.Process(
                context,
                inputs,
                output);
        }
    }
}
