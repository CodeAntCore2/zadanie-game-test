using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class BrickGridAuthoring : MonoBehaviour
{
    public GameObject brickPrefab;

    public int rows = 5;
    public int columns = 10;

    public float spacing = 1.1f;

}

public class BrickGridBaker : Baker<BrickGridAuthoring>
{
    public override void Bake(BrickGridAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);

        AddComponent(entity, new BrickSpawner
        {
            BrickPrefab = GetEntity(authoring.brickPrefab, TransformUsageFlags.Dynamic),
            Rows = authoring.rows,
            Columns = authoring.columns,
            Spacing = authoring.spacing
        });
    }
}
