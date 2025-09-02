using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Slider manaSlider;
    [SerializeField] float maxMana = 100f;
    [SerializeField] float currentMana = 30f;
    [SerializeField] private Vector2 currentCheckpoint;


    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        GetCurrentCheckPoint();
    }
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
        GetCurrentCheckPoint();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetCurrentCheckPoint();
        PlayerRespawn();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            StartCoroutine(TakeLifeWithDelay());
        }
        else
        {
            StartCoroutine(OutOfLifeWithDelay());
        }
    }

    IEnumerator TakeLifeWithDelay()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        TakeLife();
    }

    IEnumerator OutOfLifeWithDelay()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Debug.Log("Game Over");
        livesText.text = "Game Over!";
        // ResetGameSession();
        FindObjectOfType<ScenesPersist>().ResetScenesPersist();
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    private void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    private void PlayerRespawn()
    {
        FindObjectOfType<PlayerMovement>().transform.position = currentCheckpoint;
    }

    private void ResetGameSession()
    {
        score = 0;
        playerLives = 3;
        FindObjectOfType<ScenesPersist>().ResetScenesPersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddToLife()
    {
        playerLives++;
        livesText.text = playerLives.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void AddToMana(float pointsToAdd)
    {
        if (currentMana >= maxMana)
        {
            return;
        }
        currentMana += pointsToAdd;
        manaSlider.value = currentMana;
    }

    public void UseMana(float pointsToUse)
    {
        if (currentMana == 0)
        {
            return;
        }
        currentMana -= pointsToUse;
        manaSlider.value = currentMana;
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }
    
    public void SetCheckPoint(Vector2 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }

    public void GetCurrentCheckPoint()
    {   
        if (currentCheckpoint == Vector2.zero)
        {
            SetCheckPoint(FindObjectOfType<MapInfo>().GetSpawnPoint());
        }
    }

    public void ResetCheckPoint()
    {
        currentCheckpoint = Vector2.zero;
    }

    public Vector2 GetCheckPoint()
    {
        return currentCheckpoint;
    }
}
