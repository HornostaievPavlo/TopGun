using TMPro;
using UnityEngine;

public class StartTextController : MonoBehaviour
{
    [SerializeField] private TMP_Text startText;

    public float startTextSpeed;

    [SerializeField] private Transform player;


    private void Update()
    {
        ControlsTextMovement();
    }

    private void ControlsTextMovement()
    {
        float upScreenBorder = (Screen.height / 2) - 50;

        startText.rectTransform.Translate(Vector3.up * startTextSpeed * Time.deltaTime);

        if (startText.rectTransform.anchoredPosition.y >= upScreenBorder)
        {
            startText.rectTransform.anchoredPosition = new Vector3(0, upScreenBorder, 0);
        }
    }
}
