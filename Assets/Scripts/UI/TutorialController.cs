using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Image controls;

    public Button button;

    private void Start()
    {
        controls.gameObject.SetActive(true);

        StartCoroutine(ShowButton());
    }

    public IEnumerator ShowButton()
    {
        float secondsToReadTutorial = 5f;

        yield return new WaitForSeconds(secondsToReadTutorial);

        button.gameObject.SetActive(true);
    }
}
