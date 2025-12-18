using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BrickDamageSystem : ISystem
{
  public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BallData>();
    }

    public void OnUpdate(ref SystemState state)
    {
        BrickDamageJob job = new BrickDamageJob {DeltaTime = SystemAPI.Time.DeltaTime };

        job.ScheduleParallel();
    }

    public partial struct BrickDamageJob: IJobEntity
    {
        public float DeltaTime;
        public void Execute(ref BallData ball, ref LocalTransform transform )
        {
            transform = transform.Translate(ball.Direction*ball.Speed*DeltaTime);
        }
    }
}
