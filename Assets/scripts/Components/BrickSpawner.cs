using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BrickSpawner : IComponentData
{
    public Entity BrickPrefab;
    public int Rows;
    public int Columns;
    public float Spacing;
}
