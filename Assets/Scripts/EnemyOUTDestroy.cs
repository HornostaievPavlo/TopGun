using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOUTDestroy : MonoBehaviour
{
    private int downBoundary = 8;
    private int rightLoseBoundary = 22;

    void Update()
    {
        if (transform.position.y < -downBoundary)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -rightLoseBoundary)
        {
            FindObjectOfType<GameManager>().EndGame();
            Destroy(gameObject);
        }
    }
}
