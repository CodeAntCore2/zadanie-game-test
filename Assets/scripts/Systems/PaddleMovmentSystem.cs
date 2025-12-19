using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;


public partial struct PaddleMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float input = SystemAPI.GetSingleton<PaddleInput>().Move;
        float DeltaTime = SystemAPI.Time.DeltaTime;

        var job = new PaddleMovementJob
        {
            Input = input,
            DeltaTime = DeltaTime
        };

        state.Dependency = job.ScheduleParallel(state.Dependency);
    }

    public partial struct PaddleMovementJob : IJobEntity
    {
        public float Input;
        public float DeltaTime;

        void Execute(ref LocalTransform transform, in PaddleData paddle)
        {
            transform.Position.x += Input * paddle.Speed * DeltaTime;
            transform.Position.x = math.clamp(
                transform.Position.x,
                -9,
                9
            );
        }
    }

}

