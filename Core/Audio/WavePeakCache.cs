namespace HighAudioGen.Core.Audio;

public sealed class WavePeakCache
{
    public required Peak[] Peaks
    {
        get;
        init;
    }

    public required int SamplesPerPeak
    {
        get;
        init;
    }

    public int Count
        => Peaks.Length;
}
