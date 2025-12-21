using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct BrickSpawnSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);



        foreach (var (spawner, entity) in
                 SystemAPI.Query<RefRO<BrickSpawner>>().WithEntityAccess())
        {
            for (int y = 0; y < spawner.ValueRO.Rows; y++)
            {
                for (int x = 0; x < spawner.ValueRO.Columns; x++)
                {
                    Entity brick = ecb.Instantiate(spawner.ValueRO.BrickPrefab);

                    float3 pos = new float3(
                       x * spawner.ValueRO.Spacing -8,
                       - y * spawner.ValueRO.Spacing +6,
                        9
                    );

                    ecb.SetComponent(brick, LocalTransform.FromPosition(pos));
                }
            }

       
            ecb.DestroyEntity(entity);
        }


        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

