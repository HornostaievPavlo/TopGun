using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunTimeText : MonoBehaviour
{
    public GameManager gameManager;

    public Text emptyText;    

    string lose = "OBJECTIVE FAILED";

    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver == true)
        {
            emptyText.text = lose;
        }
       

    }
}
