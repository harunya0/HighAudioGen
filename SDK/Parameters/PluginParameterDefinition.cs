namespace HighAudioGen.SDK.Parameters;

/// <summary>
/// プラグインパラメータ定義の基底クラスです。
/// </summary>
public abstract record PluginParameterDefinition
{
    /// <summary>
    /// パラメータID
    /// 例: volume, gain, pan
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// UIに表示する名前
    /// </summary>
    public required string DisplayName { get; init; }

    /// <summary>
    /// 一行で分かる簡潔な説明
    /// </summary>
    public string? Summary { get; init; }

    /// <summary>
    /// 詳細な説明
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// UIでのグループ名
    /// </summary>
    public string? Group { get; init; }

    /// <summary>
    /// UIで表示する順番
    /// </summary>
    public int Order { get; init; }

    /// <summary>
    /// パラメータの種類
    /// </summary>
    public abstract PluginParameterType Type { get; }
}
