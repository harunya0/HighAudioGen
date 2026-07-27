namespace HighAudioGen.SDK.Validation;

/// <summary>
/// パラメータ検証コードを定義します。
/// </summary>
public static class ValidationCodes
{
    /// <summary>
    /// 最小値未満です。
    /// </summary>
    public const string BelowMinimum = "HAG0001";

    /// <summary>
    /// 最大値を超えています。
    /// </summary>
    public const string AboveMaximum = "HAG0002";

    /// <summary>
    /// 推奨最小値未満です。
    /// </summary>
    public const string BelowRecommendedMinimum = "HAG0003";

    /// <summary>
    /// 推奨最大値を超えています。
    /// </summary>
    public const string AboveRecommendedMaximum = "HAG0004";

    /// <summary>
    /// 指定されたファイルが存在しません。
    /// </summary>
    public const string FileNotFound = "File.NotFound";

    /// <summary>
    /// 指定されたディレクトリが存在しません。
    /// </summary>
    public const string DirectoryNotFound = "Directory.NotFound";

    /// <summary>
    /// 指定された列挙値は定義されていません。
    /// </summary>
    public const string InvalidEnumValue = "Enum.InvalidValue";
}
