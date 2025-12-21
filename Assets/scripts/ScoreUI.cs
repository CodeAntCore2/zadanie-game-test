using TMPro;
using Unity.Entities;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    EntityManager entityManager;
    EntityQuery gameDataQuery;

    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        gameDataQuery = entityManager.CreateEntityQuery(
            ComponentType.ReadOnly<GameData>());
    }

    void Update()
    {
        if (gameDataQuery.IsEmpty)
            return;

        var gameData = gameDataQuery.GetSingleton<GameData>();

        scoreText.text = $"Score: {gameData.Score}\nLives: {gameData.lives}";
    }
}
