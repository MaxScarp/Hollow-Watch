using System.Numerics;

public readonly struct SoundStimulusEvent
{
    public readonly Vector3 Position;
    public readonly float Intensity;
    public readonly SoundSourceType SourceType;

    public SoundStimulusEvent(Vector3 position, float intensity, SoundSourceType sourceType)
    {
        Position = position;
        Intensity = intensity;
        SourceType = sourceType;
    }
}