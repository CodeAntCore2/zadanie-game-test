using Unity.Entities;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateAfter(typeof(BrickCollisionSystem))]
public partial struct ApplyScoreSystem : ISystem
{
    EntityQuery _scoreQuery;

    public void OnCreate(ref SystemState state)
    {
        _scoreQuery = state.GetEntityQuery(typeof(ScoreIncrement));
    }

    public void OnUpdate(ref SystemState state)
    {
        int count = _scoreQuery.CalculateEntityCount();
        if (count == 0)
            return;

        foreach (var gameData in SystemAPI.Query<RefRW<GameData>>())
            gameData.ValueRW.Score += count;

        state.EntityManager.DestroyEntity(_scoreQuery);
    }
}