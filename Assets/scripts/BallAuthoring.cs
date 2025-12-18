using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BallAuthoring : MonoBehaviour
{
    public float Speed;
    public float3 Direction;
}

public class BallBaker : Baker<BallAuthoring>
{
    public override void Bake(BallAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new BallData
        {
            Speed = authoring.Speed,
            Direction = authoring.Direction

        });
    }
}
