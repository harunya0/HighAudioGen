using HighAudioGen.AudioEngine.Graph;
using HighAudioGen.SDK.Graph;

namespace HighAudioGen.AudioEngine.Execution;

public sealed class AudioEngine
{
    private readonly GraphValidator _validator = new();
    private readonly BufferAllocator _allocator = new();
    private readonly GraphExecutor _executor = new();

    public void Render(
        AudioGraph graph,
        AudioProcessContext context)
    {
        ArgumentNullException.ThrowIfNull(graph);
        ArgumentNullException.ThrowIfNull(context);

        _validator.Validate(graph);
        _allocator.Allocate(graph, context);
        _executor.Execute(graph, context);
    }
}
