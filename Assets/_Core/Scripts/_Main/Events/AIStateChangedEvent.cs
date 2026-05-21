public readonly struct AIStateChangedEvent
{
    public readonly CreatureState NewState;
    public readonly CreatureState PreviousState;

    public AIStateChangedEvent(CreatureState newState, CreatureState previousState)
    {
        NewState = newState;
        PreviousState = previousState;
    }
}
