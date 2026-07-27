namespace HighAudioGen.SDK.Parameters;

/// <summary>
/// ディレクトリ型の定義です。
/// </summary>
public sealed record DirectoryParameterDefinition
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
        => PluginParameterType.Directory;
}
