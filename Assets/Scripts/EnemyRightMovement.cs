using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRightMovement : MonoBehaviour
{
    public float enemyHorSpeed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * enemyHorSpeed);
    }
}
