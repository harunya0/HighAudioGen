using NAudio.Wave;

namespace HighAudioGen.Core.Audio;

public static class WaveFileReader
{
    public static WaveData Read(string path)
    {
        using AudioFileReader reader = new(path);

        List<float> samples = [];

        float[] buffer = new float[4096];

        int read;

        while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
        {
            for (int i = 0; i < read; i++)
            {
                samples.Add(buffer[i]);
            }
        }

        WaveData data =
            new()
            {
                Samples = samples.ToArray(),
                SampleRate = reader.WaveFormat.SampleRate,
                Channels = reader.WaveFormat.Channels
            };

        data.PeakCache =
            WavePeakCacheBuilder.Build(
                data,
                512);

        return data;
    }
}
