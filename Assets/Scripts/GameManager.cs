using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;

    public bool killAll;

    public float restartDelay;

    public void EndGame() 
    {
        if (isGameOver == false) 
        {
            isGameOver = true;
            Invoke("RestartLevel", restartDelay);
        }       
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
