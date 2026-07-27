namespace HighAudioGen.SDK.Buffers;

public sealed class AudioBuffer
{
    private readonly float[][] _channels;

    public AudioBuffer(int channelCount, int frameCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(channelCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(frameCount);

        _channels = new float[channelCount][];

        for (int i = 0; i < channelCount; i++)
        {
            _channels[i] = new float[frameCount];
        }
    }

    public int ChannelCount => _channels.Length;

    public int FrameCount => _channels[0].Length;

    public float[] GetChannel(int index)
    {
        return _channels[index];
    }

    public void Clear()
    {
        foreach (var channel in _channels)
        {
            Array.Clear(channel);
        }
    }
}
