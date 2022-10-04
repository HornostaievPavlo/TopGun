using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReloader : MonoBehaviour
{   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogWarning("SCENE RELOADING");
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
