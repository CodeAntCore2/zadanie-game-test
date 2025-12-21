using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct GameData : IComponentData
{
    public int Score;
    public int lives;

}
