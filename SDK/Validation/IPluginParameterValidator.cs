using HighAudioGen.SDK.Parameters;
using HighAudioGen.SDK.Results;


namespace HighAudioGen.SDK.Validation;

/// <summary>
/// プラグインパラメーターの検証を行います
/// </summary>
public interface IPluginParameterValidator
{
    ParameterValidationResult Validate(
        DoubleParameterDefinition definition,
        double value);

    ParameterValidationResult Validate(
        IntegerParameterDefinition definition,
        int value);

    ParameterValidationResult Validate(
        BooleanParameterDefinition definition,
        bool value);

    ParameterValidationResult Validate(
        StringParameterDefinition definition,
        string value);

    ParameterValidationResult Validate(
        FileParameterDefinition definition,
        string value);

    ParameterValidationResult Validate(
        DirectoryParameterDefinition definition,
        string value);

    ParameterValidationResult Validate<TEnum>(
        EnumParameterDefinition<TEnum> definition,
        TEnum value)
        where TEnum : struct, Enum;
}
