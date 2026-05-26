public interface ICreatureState
{
    void Enter(CreatureContext ctx);
    CreatureState Update(CreatureContext ctx, float deltaTime);
    void Exit(CreatureContext ctx);
}
