using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;


public partial struct PaddleMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float input = SystemAPI.GetSingleton<PaddleInput>().Move;

        var job = new PaddleMovementJob
        {
            Input = input
        };

        state.Dependency = job.ScheduleParallel(state.Dependency);
    }

    public partial struct PaddleMovementJob : IJobEntity
    {
        public float Input;

        void Execute(ref PhysicsVelocity velocity, in PaddleData paddle)
        {
            velocity.Linear.x = Input * paddle.Speed;
            velocity.Linear.y = 0f;
            velocity.Linear.z = 0f;
        }
    }

}

