using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    [SerializeField] private TMP_Text mainText;

    [SerializeField] private TMP_Text enemiesCounter;

    [SerializeField] private GameObject menu;

    private void Start()
    {
        StartCoroutine(ShowCounterText());
    }

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
            enemiesCounter.gameObject.SetActive(false);

            mainText.text = "Level failed";

            menu.SetActive(true);
        }
        else if (isVictory)
        {
            enemiesCounter.gameObject.SetActive(false);

            mainText.text = "Level completed";

            menu.SetActive(true);
        }
        else
        {
            enemiesCounter.text = scoreManager.enemiesCounter.ToString() + " enemies left";
        }
    }

    private IEnumerator ShowCounterText()
    {
        yield return new WaitForSeconds(10f);

        enemiesCounter.gameObject.SetActive(true);
    }
}
