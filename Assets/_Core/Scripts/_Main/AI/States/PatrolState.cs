using System.Numerics;

public class PatrolState : ICreatureState
{
    public void Enter(CreatureContext ctx)
    {
        if (ctx.PatrolWaypoints.Length == 0)
        {
            return;
        }

        ctx.SetDestination?.Invoke(ctx.PatrolWaypoints[ctx.CurrentWaypointIndex]);
    }

    public void Exit(CreatureContext ctx)
    {
    }

    public CreatureState Update(CreatureContext ctx, float deltaTime)
    {
        if (ctx.ConsumePendingSound(out float intensity, out _))
        {
            if (intensity >= ctx.AlertThreshold)
            {
                return CreatureState.Alert;
            }
        }

        if (ctx.PatrolWaypoints.Length == 0)
        {
            return CreatureState.Patrol;
        }

        Vector3 target = ctx.PatrolWaypoints[ctx.CurrentWaypointIndex];
        float distance = Vector3.Distance(ctx.CreaturePosition, target);

        if (distance < ctx.WaypointReachedThreshold)
        {
            ctx.CurrentWaypointIndex = (ctx.CurrentWaypointIndex + 1) % ctx.PatrolWaypoints.Length;
            ctx.SetDestination?.Invoke(ctx.PatrolWaypoints[ctx.CurrentWaypointIndex]);
        }

        return CreatureState.Patrol;
    }
}
