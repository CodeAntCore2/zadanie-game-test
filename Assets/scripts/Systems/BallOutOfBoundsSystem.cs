using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;


public partial struct BallOutOfBoundsSystem: ISystem
{
    public void OnUpdate(ref SystemState state)
    {

        var rng = new Unity.Mathematics.Random(
           (uint)SystemAPI.Time.ElapsedTime.GetHashCode() + 1);


        var job = new BallOutoFBoundsJob
        {
            Rng = rng
        };

        state.Dependency = job.ScheduleParallel(state.Dependency);
    }

    public partial struct BallOutoFBoundsJob : IJobEntity
    {

        public Unity.Mathematics.Random Rng;
        void Execute(ref LocalTransform transform, in BallData Ball , ref PhysicsVelocity velocity )
        {
           

            if(transform.Position.y<-8)
            {
                // change ball postiton
                transform.Position.x = 0;
                transform.Position.y = -4;

                // change ball speed

                float x = Rng.NextFloat(-10f, 10f);



                velocity.Linear = new float3(x,5,0);
            }

         
        }
    }

}

