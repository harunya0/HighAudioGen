namespace HighAudioGen.SDK.Plugins;

/// <summary>
/// プラグインのメタデータを表します。
/// </summary>
public sealed record PluginMetadata
{
    /// <summary>
    /// 一意なプラグインID
    /// 例: com.highaudiogen.codec.wave
    /// </summary>
    public required string PluginId { get; init; }

    /// <summary>
    /// UIに表示する名前
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// 一行で分かる簡潔な説明
    /// </summary>
    public string? Summary { get; init; }

    /// <summary>
    /// 詳細な説明
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// バージョン
    /// </summary>
    public required string Version { get; init; }

    /// <summary>
    /// 作者
    /// </summary>
    public required string Author { get; init; }

    /// <summary>
    /// 公式サイト
    /// </summary>
    public Uri? Website { get; init; }

    /// <summary>
    /// ライセンス名
    /// </summary>
    public string? License { get; init; }
}
