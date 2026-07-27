namespace HighAudioGen.Core.Audio;

public sealed class WaveData
{
    public required float[] Samples
    {
        get;
        init;
    }

    public required int SampleRate
    {
        get;
        init;
    }

    public required int Channels
    {
        get;
        init;
    }

    public WavePeakCache? PeakCache
    {
        get;
        set;
    }

    public int SampleCount
        => Samples.Length;
}
