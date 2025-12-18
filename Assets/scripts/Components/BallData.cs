using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BallData : IComponentData
{
    public float Speed;
    public float3 Direction;
    
}
