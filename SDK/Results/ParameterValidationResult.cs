namespace HighAudioGen.SDK.Results;

/// <summary>
/// パラメータ検証結果を表します。
/// </summary>
public sealed record ParameterValidationResult
{
    /// <summary>
    /// 検証結果一覧
    /// </summary>
    public required IReadOnlyList<ParameterValidationIssue> Issues { get; init; }

    /// <summary>
    /// エラーが存在しないかどうか
    /// </summary>
    public bool IsValid =>
        Issues.All(issue => issue.Severity != ParameterValidationSeverity.Error);

    /// <summary>
    /// 警告が存在するかどうか
    /// </summary>
    public bool HasWarnings =>
        Issues.Any(i => i.Severity == ParameterValidationSeverity.Warning);
}
