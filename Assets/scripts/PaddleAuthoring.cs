using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PaddleAuthoring : MonoBehaviour
{
    public float Speed;

}

public class PaddleBaker : Baker<PaddleAuthoring>
{
    public override void Bake(PaddleAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new PaddleData
        {
            Speed = authoring.Speed,
            

        });
    }
}
