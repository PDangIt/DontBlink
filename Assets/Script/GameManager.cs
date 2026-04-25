using UnityEngine;
using TMPro;

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

    public void PlayerDied()
    {
        if (isDead) return;

        isDead = true;
        deathScreen.SetActive(true);

        int seconds = Mathf.FloorToInt(survivalTime);
        scoreText.text = "You survived: " + seconds + " seconds";

        Time.timeScale = 0f;
    }
}