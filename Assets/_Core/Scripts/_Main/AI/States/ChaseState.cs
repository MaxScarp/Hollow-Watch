public class ChaseState : ICreatureState
{
    public void Enter(CreatureContext ctx)
    {
    }

    public CreatureState Update(CreatureContext ctx, float deltaTime)
    {
        return CreatureState.Alert;
    }

    public void Exit(CreatureContext ctx)
    {
    }
}
