using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class GameDataAuthoring : MonoBehaviour
{
    public int Score;
    public int lives;

}

public class GameDataBaker : Baker<GameDataAuthoring>
{
    public override void Bake(GameDataAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new GameData
        {
           Score=authoring.Score,
           lives=authoring.lives
            

        });
    }
}
