namespace HighAudioGen.SDK.Parameters;

/// <summary>
/// 列挙型パラメータの定義を表します。
/// </summary>
/// <typeparam name="TEnum">列挙型</typeparam>
public sealed record EnumParameterDefinition<TEnum> : PluginParameterDefinition
    where TEnum : struct, Enum
{
    /// <summary>
    /// デフォルト値
    /// </summary>
    public required TEnum DefaultValue { get; init; }

    /// <summary>
    /// パラメータの種類
    /// </summary>
    public override PluginParameterType Type
        => PluginParameterType.Enum;
}
