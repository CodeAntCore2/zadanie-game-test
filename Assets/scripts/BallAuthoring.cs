using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BallAuthoring : MonoBehaviour
{
   
}

public class BallBaker : Baker<BallAuthoring>
{
    public override void Bake(BallAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new BallData
        {
        

        });
    }
}
