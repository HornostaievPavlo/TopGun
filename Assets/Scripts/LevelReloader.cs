using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReloader : MonoBehaviour
{   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogWarning("SCENE RELOADING");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
