using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject deathScreen;
    public TextMeshProUGUI scoreText;

    private float survivalTime;
    private bool isDead;

    void Update()
    {
        if (!isDead)
            survivalTime += Time.deltaTime;
    }

    void Awake()
    {
        Time.timeScale = 1f;

        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
    }

    public void PlayerDied()
    {
        if (isDead) return;

        isDead = true;
        deathScreen.SetActive(true);

        int seconds = Mathf.FloorToInt(survivalTime);
        scoreText.text = "You survived: " + seconds + " seconds";

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        survivalTime = 0f;
        isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}