using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Image controls;

    [SerializeField] private Button button;

    private void Awake()
    {
        controls.gameObject.SetActive(true);

        StartCoroutine(ShowButton());
    }

    private IEnumerator ShowButton()
    {
        float secondsToReadTutorial = 5f;

        yield return new WaitForSeconds(secondsToReadTutorial);

        button.gameObject.SetActive(true);
    }    
}
