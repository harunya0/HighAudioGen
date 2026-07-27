namespace HighAudioGen.SDK.Parameters;

/// <summary>
/// 文字列の定義です。
/// </summary>
public sealed record StringParameterDefinition
    : PluginParameterDefinition
{
    /// <summary>
    /// パラメーターの規定値
    /// </summary>
    public required string DefaultValue { get; init; }

    /// <summary>
    /// 最低文字数
    /// </summary>
    public int? MinimumLength { get; init; }

    /// <summary>
    /// 最大文字数
    /// </summary>
    public int? MaximumLength { get; init; }

    public override PluginParameterType Type
        => PluginParameterType.String;
}
