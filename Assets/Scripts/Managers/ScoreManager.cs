using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<GameEntity> enemiesArr;

    public HealthSystem playerHealthSystem;

    public int enemiesCounter;

    private void Update()
    {
        UpdateEnemiesList();

        enemiesCounter = enemiesArr.Count;
    }

    private void UpdateEnemiesList()
    {
        for (int i = 0; i < enemiesArr.Count; i++)
        {
            if (enemiesArr[i] == null)
            {
                enemiesArr.RemoveAt(i);
            }
        }
    }

    public bool IsLevelWon()
    {
        if (!playerHealthSystem.isDead && enemiesArr.Count == 0)
        {
            return true;
        }

        return false;
    }
}
