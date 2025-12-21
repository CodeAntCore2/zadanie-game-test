using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct BrickCollisionSystem : ISystem
{
    ComponentLookup<BrickData> _brickLookup;
    ComponentLookup<BallData> _ballLookup;

    public void OnCreate(ref SystemState state)
    {
        _brickLookup = state.GetComponentLookup<BrickData>(false);
        _ballLookup = state.GetComponentLookup<BallData>(true);
    }

    public void OnUpdate(ref SystemState state)
    {
        _brickLookup.Update(ref state);
        _ballLookup.Update(ref state);

        var simulation = SystemAPI.GetSingleton<SimulationSingleton>();
        var gameDataEntity = SystemAPI.GetSingletonEntity<GameData>();

        var ecb = SystemAPI
            .GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged)
            .AsParallelWriter();

        var job = new CollisionJob
        {
            BrickLookup = _brickLookup,
            BallLookup = _ballLookup,
            GameDataEntity = gameDataEntity,
            ECB = ecb
        };

        state.Dependency = job.Schedule(simulation, state.Dependency);
    }

    [BurstCompile]
    public struct CollisionJob : ICollisionEventsJob
    {
        [NativeDisableParallelForRestriction]
        public ComponentLookup<BrickData> BrickLookup;

        [ReadOnly]
        public ComponentLookup<BallData> BallLookup;

        public Entity GameDataEntity;

        public EntityCommandBuffer.ParallelWriter ECB;

        public void Execute(CollisionEvent collisionEvent)
        {
            Entity brick = Entity.Null;

            if (BrickLookup.HasComponent(collisionEvent.EntityA) &&
                BallLookup.HasComponent(collisionEvent.EntityB))
            {
                brick = collisionEvent.EntityA;
            }
            else if (BrickLookup.HasComponent(collisionEvent.EntityB) &&
                     BallLookup.HasComponent(collisionEvent.EntityA))
            {
                brick = collisionEvent.EntityB;
            }

            if (brick == Entity.Null)
                return;

            var brickData = BrickLookup[brick];
            brickData.Health--;
            BrickLookup[brick] = brickData;

            if (brickData.Health <= 0)
            {
                ECB.DestroyEntity(0, brick);
                ECB.SetComponent(0, GameDataEntity, new ScoreIncrement { Value = 1 });

            }
        }
    }
}
