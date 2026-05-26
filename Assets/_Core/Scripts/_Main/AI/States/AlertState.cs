using System.Numerics;

public class AlertState : ICreatureState
{
    public void Enter(CreatureContext ctx)
    {
        ctx.AlertTimer = ctx.AlertDuration;
        ctx.SetDestination?.Invoke(ctx.PendingSoundOrigin);
    }

    public CreatureState Update(CreatureContext ctx, float deltaTime)
    {
        ctx.AlertTimer -= deltaTime;

        if (ctx.ConsumePendingSound(out float intensity, out Vector3 origin))
        {
            if (intensity >= ctx.AlertThreshold)
            {
                ctx.PendingSoundOrigin = origin;
                ctx.AlertTimer = ctx.AlertDuration;
                ctx.SetDestination?.Invoke(ctx.PendingSoundOrigin);
            }
        }

        if (ctx.AlertTimer <= 0.0f)
        {
            return CreatureState.Patrol;
        }

        return CreatureState.Alert;
    }

    public void Exit(CreatureContext ctx)
    {
        ctx.AlertTimer = 0.0f;
    }
}
