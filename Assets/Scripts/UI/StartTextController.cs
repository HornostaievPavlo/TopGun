using System.Collections;
using TMPro;
using UnityEngine;

public class StartTextController : MonoBehaviour
{
    public TMP_Text startText;

    public float startTextSpeed;

    private void Update()
    {
        StartTextMovement();
    }

    public void StartTextMovement()
    {
        float upScreenBorder = (Screen.height / 2) - 50;

        startText.rectTransform.Translate(Vector3.up * startTextSpeed * Time.deltaTime);

        if (startText.rectTransform.anchoredPosition.y >= upScreenBorder)
        {
            startText.rectTransform.anchoredPosition = new Vector3(0, upScreenBorder, 0);
        }

        StartCoroutine(DestroyText());
    }

    private IEnumerator DestroyText()
    {
        yield return new WaitForSecondsRealtime(8f);

        Destroy(startText);
    }
}
