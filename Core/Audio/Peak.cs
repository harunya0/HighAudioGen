namespace HighAudioGen.Core.Audio;

public readonly struct Peak
{
    public Peak(
        float min,
        float max)
    {
        Min = min;
        Max = max;
    }

    public float Min
    {
        get;
    }

    public float Max
    {
        get;
    }
}
