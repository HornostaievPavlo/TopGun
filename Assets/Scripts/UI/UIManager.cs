using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private TMP_Text mainText;

    private void Update()
    {
        SetCounterText();
    }

    private void SetCounterText()
    {
        if (scoreManager.playerHealthSystem.isDead)
        {
            mainText.text = "Level failed";
        }
        else if (scoreManager.IsLevelWon())
        {
            mainText.text = "Level completed";
        }
        else
        {
            mainText.text = string.Empty;
        }
    }
}
