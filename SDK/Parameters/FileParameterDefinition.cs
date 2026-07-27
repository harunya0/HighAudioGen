namespace HighAudioGen.SDK.Parameters;

/// <summary>
/// ファイルの定義です。
/// </summary>
public sealed record FileParameterDefinition
    : PluginParameterDefinition
{
    /// <summary>
    /// パラメーターの規定値
    /// </summary>
    public required string DefaultValue { get; init; }

    /// <summary>
    /// 存在しなくてはいけないか
    /// </summary>
    public bool MustExist { get; init; }


    public override PluginParameterType Type
        => PluginParameterType.File;
}
