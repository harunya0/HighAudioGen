namespace HighAudioGen.SDK.Graph;

public interface ISeekAware
{
    void PrepareForSeek(long currentSample, long targetSample);
}
