using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombOUTDestroy : MonoBehaviour
{
    private int boundary = 1;
    
    void Update()
    {
        if (transform.position.x > boundary) 
        {
            Destroy(gameObject);
        }
    }
}
