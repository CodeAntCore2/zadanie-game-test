using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BrickAuthoring : MonoBehaviour
{
    public int Health;
}

public class BrickBaker : Baker<BrickAuthoring>
{
    public override void Bake(BrickAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new BrickData
        {
        Health = authoring.Health,

        });
    }
}
