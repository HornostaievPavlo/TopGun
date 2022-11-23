using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<GameEntity> enemies;

    public HealthSystem playerHealthSystem;

    public int enemiesCounter;

    private void Update()
    {
        UpdateEnemiesList();

        enemiesCounter = enemies.Count;
    }

    private void UpdateEnemiesList()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    public bool IsLevelWon()
    {
        if (!playerHealthSystem.isDead && enemies.Count == 0)
        {
            return true;
        }

        return false;
    }
}
