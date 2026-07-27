namespace HighAudioGen.SDK.Parameters;

/// <summary>
/// 真偽型の定義です。
/// </summary>
public sealed record BooleanParameterDefinition
    : PluginParameterDefinition
{
    /// <summary>
    /// パラメーターの既定値
    /// </summary>
    public required bool DefaultValue { get; init; }

    public override PluginParameterType Type
        => PluginParameterType.Boolean;
}
