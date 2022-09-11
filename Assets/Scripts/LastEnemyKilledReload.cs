using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastEnemyKilledReload : MonoBehaviour
{
    public Text enemyText;

    private string win = "OBJECTIVE COMPLETED";



    public IEnumerator nextLevel() 
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }





    private void OnTriggerEnter(Collider other)
    {
        enemyText.text = win;
        StartCoroutine(nextLevel());
    }
}
