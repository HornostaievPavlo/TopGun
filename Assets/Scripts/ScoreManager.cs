using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private List<GameEntity> enemiesArr;

    private void Start()
    {

    }

    private void Update()
    {
        CalculateEnemies();
    }

    private void CalculateEnemies()
    {
        for (int i = 0; i < enemiesArr.Count; i++)
        {
            if (enemiesArr[i] == null)
            {
                Debug.Log("Found empty");

                enemiesArr.RemoveAt(i);
            }
        }

        if (enemiesArr.Count == 0)
        {
            Debug.Log("viskas");
        }
    }
}
