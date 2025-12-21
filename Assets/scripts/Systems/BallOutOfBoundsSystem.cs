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
      
        var gameData = SystemAPI.GetSingleton<GameData>();
       
        
        var rng = new Unity.Mathematics.Random(
            (uint)(SystemAPI.Time.ElapsedTime * 1000 + 1));

        foreach (var (transform, velocity)
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRW<PhysicsVelocity>>())
        {
            if (transform.ValueRO.Position.y < -8f)
            {

                velocity.ValueRW.Linear = new float3(
                    rng.NextFloat(-10, 10), 5, 0);
              

                // Reset ball position
                transform.ValueRW.Position = new float3(0, -4, 9);



                gameData.lives--;
                SystemAPI.SetSingleton(gameData);

            }
        }

       
    }
}