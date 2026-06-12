using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinText;
    const string ENEMIES_LEFT = "Enemies Left: ";

    int enemiesLeft = 0;

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT + enemiesLeft.ToString();

        if (enemiesLeft <= 0)
        {
            youWinText.SetActive(true);
        }
    }
    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButton()
    {
        Debug.LogWarning("Unable to quit from dev Editor");
        Application.Quit();
    }
}
