using HighAudioGen.SDK.Graph;
using HighAudioGen.SDK.Parameters;

namespace HighAudioGen.SDK.Plugins;

/// <summary>
/// すべてのプラグインが実装する基本インターフェースです。
/// </summary>
public interface IPlugin : IAudioNode
{
    PluginMetadata Metadata { get; }
    IReadOnlyList<PluginParameterDefinition> Parameters { get; }
}
