using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private TMP_Text mainText;

    [SerializeField] private GameObject menu;

    private void Update()
    {
        UpdateGameUI();
    }

    private void UpdateGameUI()
    {
        bool isVictory = scoreManager.IsLevelWon();

        bool isFail = scoreManager.playerHealthSystem.isDead;

        if (isFail)
        {
            mainText.text = "Level failed";

            menu.SetActive(true);
        }
        else if (isVictory)
        {
            mainText.text = "Level completed";

            menu.SetActive(true);
        }
    }
}
