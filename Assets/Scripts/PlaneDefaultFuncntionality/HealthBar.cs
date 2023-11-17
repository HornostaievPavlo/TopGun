using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;

    [SerializeField] private Gradient gradient;

    [SerializeField] private float updateSpeed;

    private void Awake()
    {
        GetComponentInParent<HealthSystem>().OnHealthPercentChanged += HandleHealthChange;

        foregroundImage.color = gradient.Evaluate(1f);
    }

    private void HandleHealthChange(float percent)
    {
        StartCoroutine(ChangeToPercent(percent));
    }

    private IEnumerator ChangeToPercent(float percent)
    {
        float preChangePercent = foregroundImage.fillAmount;

        float elapsedTime = 0f;

        while (elapsedTime < updateSpeed)
        {
            elapsedTime += Time.deltaTime;

            foregroundImage.fillAmount = Mathf.Lerp(preChangePercent, percent, elapsedTime / updateSpeed);

            yield return null;
        }

        foregroundImage.fillAmount = percent;

        foregroundImage.color = gradient.Evaluate(foregroundImage.fillAmount);
    }
}
