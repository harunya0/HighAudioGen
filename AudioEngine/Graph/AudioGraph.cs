using HighAudioGen.SDK.Graph;
using System.Collections;

namespace HighAudioGen.AudioEngine.Graph;

public sealed class AudioGraph
{
    private readonly List<IAudioNode> _nodes = [];

    private readonly List<AudioConnection> _connections = [];

    public void AddNode(IAudioNode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        if (_nodes.Contains(node))
        {
            throw new InvalidOperationException("Node is already added.");
        }

        _nodes.Add(node);
    }

    public void Connect(
        IAudioNode sourceNode,
        IAudioPort sourcePort,
        IAudioNode destinationNode,
        IAudioPort destinationPort)
    {
        ArgumentNullException.ThrowIfNull(sourceNode);
        ArgumentNullException.ThrowIfNull(sourcePort);
        ArgumentNullException.ThrowIfNull(destinationNode);
        ArgumentNullException.ThrowIfNull(destinationPort);

        if (!_nodes.Contains(sourceNode))
        {
            throw new InvalidOperationException("Source node is not in this graph.");
        }

        if (!_nodes.Contains(destinationNode))
        {
            throw new InvalidOperationException("Destination node is not in this graph.");
        }

        _connections.Add(new AudioConnection
        {
            SourceNode = sourceNode,
            SourcePort = sourcePort,
            DestinationNode = destinationNode,
            DestinationPort = destinationPort
        });
    }

    public IReadOnlyList<AudioConnection> GetInputConnections(
        IAudioNode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        return _connections
            .Where(connection => connection.DestinationNode == node)
            .ToArray();
    }

    public IReadOnlyList<AudioConnection> GetOutputConnections(
        IAudioNode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        return _connections
            .Where(connection => connection.SourceNode == node)
            .ToArray();
    }

    public IEnumerable<IAudioNode> EnumerateNodes()
    {
        return _nodes;
    }

    public IEnumerable<AudioConnection> EnumerateConnections()
    {
        return _connections;
    }

    public IEnumerable<AudioConnection> EnumerateInputs(
        IAudioNode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        return _connections.Where(connection =>
            connection.DestinationNode == node);
    }

    public IEnumerable<AudioConnection> EnumerateOutputs(
        IAudioNode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        return _connections.Where(connection =>
            connection.SourceNode == node);
    }

    public void Connect(
        IAudioNode sourceNode,
        IAudioNode destinationNode)
    {
        ArgumentNullException.ThrowIfNull(sourceNode);
        ArgumentNullException.ThrowIfNull(destinationNode);

        if (sourceNode.Outputs.Count != 1)
        {
            throw new InvalidOperationException(
                $"{sourceNode.Id} has {sourceNode.Outputs.Count} output ports. Specify the port explicitly.");
        }

        if (destinationNode.Inputs.Count != 1)
        {
            throw new InvalidOperationException(
                $"{destinationNode.Id} has {destinationNode.Inputs.Count} input ports. Specify the port explicitly.");
        }

        Connect(
            sourceNode,
            sourceNode.Outputs.Single(),
            destinationNode,
            destinationNode.Inputs.Single());
    }
}
