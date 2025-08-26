using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;

    [Header("SFX")]
    [SerializeField] private AudioClip levelExitSFX;
    [SerializeField] private float levelExitSFXVolume = 0.15f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerMovement>().isAlive)
        {
            PlayLevelExitSFX();
            StartCoroutine(LoadLevelWithDelay());
        }
    }

    IEnumerator LoadLevelWithDelay()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        LoadLevel();
    }

    private void LoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        FindObjectOfType<ScenesPersist>().ResetScenesPersist();
        FindObjectOfType<GameSession>().SetCheckPoint(Vector2.zero);
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    
    private void PlayLevelExitSFX()
    {
        AudioSource.PlayClipAtPoint(levelExitSFX, transform.position, levelExitSFXVolume);
    }
}
