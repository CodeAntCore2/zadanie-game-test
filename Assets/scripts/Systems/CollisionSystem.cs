using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;


public partial struct CollisionSystem : ISystem
{
  public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BallData>();
    }

    public void OnUpdate(ref SystemState state)
    {
           var simulation = SystemAPI.GetSingleton<SimulationSingleton>();

        state.Dependency = new CollisionJob
        {
            BallLookup = SystemAPI.GetComponentLookup<BallData>()
        }.Schedule(simulation, state.Dependency);
    }

    public partial struct CollisionJob: ICollisionEventsJob
    {
        public float DeltaTime;
        public ComponentLookup<BallData> BallLookup;
    
        public void Execute(CollisionEvent collisionEvent)
        {
            bool aIsBall = BallLookup.HasComponent(collisionEvent.EntityA);
            bool bIsBall = BallLookup.HasComponent(collisionEvent.EntityB);

            if (!aIsBall && !bIsBall)
                return;

            Entity ball = aIsBall ? collisionEvent.EntityA : collisionEvent.EntityB;

            var ballData = BallLookup[ball];

            // Reflect direction
            ballData.Direction = math.normalize(
                math.reflect(ballData.Direction, collisionEvent.Normal)
            );

            BallLookup[ball] = ballData;
        }
    }
    
}
