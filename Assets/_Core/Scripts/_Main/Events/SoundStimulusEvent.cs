public readonly struct SoundStimulusEvent
{
    public readonly float PositionX;
    public readonly float PositionY;
    public readonly float PositionZ;

    public readonly float Intensity;
    public readonly SoundSourceType SourceType;

    public SoundStimulusEvent(float positionX, float positionY, float positionZ, float intensity, SoundSourceType sourceType)
    {
        PositionX = positionX;
        PositionY = positionY;
        PositionZ = positionZ;

        Intensity = intensity;
        SourceType = sourceType;
    }
}