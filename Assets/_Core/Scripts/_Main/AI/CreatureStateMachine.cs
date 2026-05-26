using System.Collections.Generic;

public class CreatureStateMachine
{
    private ICreatureState current;
    private CreatureState currentEnum;

    private readonly Dictionary<CreatureState, ICreatureState> states;

    public CreatureState CreatureState => currentEnum;

    public CreatureStateMachine()
    {
        states = new()
        {
            { CreatureState.Patrol, new PatrolState() },
            { CreatureState.Alert, new AlertState() },
            { CreatureState.Chase, new ChaseState() }
        };
    }

    public void Initialize(CreatureContext ctx)
    {
        currentEnum = CreatureState.Patrol;
        current = states[currentEnum];
        current.Enter(ctx);
    }

    public void Tick(CreatureContext ctx, float deltaTime)
    {
        if (current == null)
        {
            return;
        }

        CreatureState desired = current.Update(ctx, deltaTime);

        if (desired != currentEnum)
        {
            TransitionTo(desired, ctx);
        }
    }

    private void TransitionTo(CreatureState next, CreatureContext ctx)
    {
        CreatureState previous = currentEnum;

        current.Exit(ctx);
        currentEnum = next;
        current = states[currentEnum];
        current.Enter(ctx);

        ctx.OnStateChanged?.Invoke(previous, next);
        GameEventBus.Publish(new AIStateChangedEvent(next, previous));
    }
}
