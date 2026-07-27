namespace HighAudioGen.SDK.Parameters;

/// <summary>
/// 倍精度浮動小数点パラメータの定義です。
/// </summary>
public sealed record DoubleParameterDefinition
    : PluginParameterDefinition
{
    /// <summary>
    /// パラメータの既定値
    /// </summary>
    public required double DefaultValue { get; init; }

    /// <summary>
    /// 許容される最小値
    /// </summary>
    public double? Minimum { get; init; }

    /// <summary>
    /// 許容される最大値
    /// </summary>
    public double? Maximum { get; init; }

    /// <summary>
    /// 推奨される最小値
    /// </summary>
    public double? RecommendedMinimum { get; init; }

    /// <summary>
    /// 推奨される最大値
    /// </summary>
    public double? RecommendedMaximum { get; init; }

    /// <summary>
    /// UIでの増減幅
    /// </summary>
    public double? Step { get; init; }

    /// <summary>
    /// 単位
    /// 例: Hz, dB, %, ms
    /// </summary>
    public string? Unit { get; init; }

    public override PluginParameterType Type
        => PluginParameterType.Double;
}
