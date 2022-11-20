using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<GameEntity> enemiesArr;

    public HealthSystem playerHealthSystem;

    private void Update()
    {
        UpdateEnemiesList();

        IsLevelWon();
    }

    private void UpdateEnemiesList()
    {
        for (int i = 0; i < enemiesArr.Count; i++)
        {
            if (enemiesArr[i] == null)
            {
                Debug.Log("Found empty");

                enemiesArr.RemoveAt(i);
            }
        }
    }

    public bool IsLevelWon()
    {
        if (playerHealthSystem.isDead)
        {
            Debug.Log("Player is dead");
            return false;
        }
        
        else if (!playerHealthSystem.isDead && enemiesArr.Count == 0)
        {
            Debug.Log("All enemies are dead");
            return true;
        }

        return false;
    }
}
