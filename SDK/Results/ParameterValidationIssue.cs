namespace HighAudioGen.SDK.Results;

/// <summary>
/// パラメータ検証時に発生した問題を表します。
/// </summary>
public sealed record ParameterValidationIssue
{
    /// <summary>
    /// エラーコード
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// パラメータID
    /// </summary>
    public string? ParameterId { get; init; }

    /// <summary>
    /// 重要度
    /// </summary>
    public required ParameterValidationSeverity Severity { get; init; }

    /// <summary>
    /// メッセージ
    /// </summary>
    public required string Message { get; init; }
}
