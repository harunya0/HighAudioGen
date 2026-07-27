namespace HighAudioGen.SDK.Parameters;

/// <summary>
/// 整数パラメータの定義です。
/// </summary>
public sealed record IntegerParameterDefinition
    : PluginParameterDefinition
{
    /// <summary>
    /// パラメータの既定値
    /// </summary>
    public required int DefaultValue { get; init; }

    /// <summary>
    /// 許容される最小値
    /// </summary>
    public int? Minimum { get; init; }

    /// <summary>
    /// 許容される最大値
    /// </summary>
    public int? Maximum { get; init; }

    /// <summary>
    /// 推奨される最小値
    /// </summary>
    public int? RecommendedMinimum { get; init; }

    /// <summary>
    /// 推奨される最大値
    /// </summary>
    public int? RecommendedMaximum { get; init; }

    /// <summary>
    /// UIでの増減幅
    /// </summary>
    public int? Step { get; init; }

    /// <summary>
    /// 単位
    /// 例: Hz, dB, %, ms
    /// </summary>
    public string? Unit { get; init; }

    public override PluginParameterType Type
        => PluginParameterType.Integer;
}
