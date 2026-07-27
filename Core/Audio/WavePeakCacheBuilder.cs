namespace HighAudioGen.Core.Audio;

public static class WavePeakCacheBuilder
{
    public static WavePeakCache Build(
        WaveData wave,
        int samplesPerPeak)
    {
        ArgumentNullException.ThrowIfNull(wave);

        if (samplesPerPeak <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(samplesPerPeak));
        }

        Peak[] peaks =
            new Peak[
                (wave.Samples.Length + samplesPerPeak - 1)
                / samplesPerPeak];

        int peakIndex = 0;

        for (int i = 0;
             i < wave.Samples.Length;
             i += samplesPerPeak)
        {
            float min = float.MaxValue;
            float max = float.MinValue;

            int end =
                Math.Min(
                    i + samplesPerPeak,
                    wave.Samples.Length);

            for (int j = i; j < end; j++)
            {
                float sample = wave.Samples[j];

                if (sample < min)
                {
                    min = sample;
                }

                if (sample > max)
                {
                    max = sample;
                }
            }

            peaks[peakIndex++] =
                new Peak(min, max);
        }

        return new WavePeakCache
        {
            Peaks = peaks,
            SamplesPerPeak = samplesPerPeak
        };
    }
}
