using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public GameObject deathScreen;
    public TextMeshProUGUI scoreText;

    public string gameSceneName = "SceneB";
    public Transform player;
    public Vector3 restartPosition = new Vector3(0, 2, -10);

    private float survivalTime;
    private bool isDead;
    private UnityEngine.XR.InputDevice leftHand;

    void Awake()
    {
        Time.timeScale = 1f;
        FindUIReferences();

        if (deathScreen != null)
            deathScreen.SetActive(false);
    }

    void Update()
    {
        if (!isDead)
        {
            survivalTime += Time.deltaTime;
            return;
        }

        TryFindLeftController();

        bool xPressed = false;
        bool yPressed = false;

        leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out xPressed);
        leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out yPressed);

        if (xPressed || yPressed)
        {
            RestartGame();
        }
    }

    void FindUIReferences()
    {
        if (deathScreen == null)
            deathScreen = GameObject.Find("Death Screen");

        if (scoreText == null && deathScreen != null)
            scoreText = deathScreen.GetComponentInChildren<TextMeshProUGUI>(true);
    }

    void TryFindLeftController()
    {
        if (leftHand.isValid) return;

        leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    public void PlayerDied()
    {
        if (isDead) return;

        FindUIReferences();

        isDead = true;

        if (deathScreen != null)
            deathScreen.SetActive(true);

        int seconds = Mathf.FloorToInt(survivalTime);

        if (scoreText != null)
            scoreText.text = "You survived: " + seconds + " seconds\nPress X or Y to restart";

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isDead = false;
        survivalTime = 0f;

        if (deathScreen != null)
            deathScreen.SetActive(false);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        if (XRSettings.isDeviceActive)
        {
            // VR: keep SceneA loaded, only reload SceneB
            StartCoroutine(RestartSceneB());
        }
        else
        {
            // PC: clean reload SceneB normally
            SceneManager.LoadScene(gameSceneName);
        }
    }
    IEnumerator RestartSceneB()
    {
        Scene sceneB = SceneManager.GetSceneByName(gameSceneName);

        if (sceneB.isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(sceneB);
        }

        yield return SceneManager.LoadSceneAsync(gameSceneName, LoadSceneMode.Additive);

        FindUIReferences();

        if (deathScreen != null)
            deathScreen.SetActive(false);

        if (player != null)
        {
            player.position = restartPosition;
            player.rotation = Quaternion.identity;
        }
    }
}