using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Vector2 checkPointPosition;
    [Header("SFX")]
    [SerializeField] private AudioClip levelExitSFX;
    [SerializeField] private float levelExitSFXVolume = 0.15f;

    private void Start()
    {
        checkPointPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameSession>().SetCheckPoint(checkPointPosition);
            PlayLevelExitSFX();
        }
    }
    
    private void PlayLevelExitSFX()
    {
        AudioSource.PlayClipAtPoint(levelExitSFX, transform.position, levelExitSFXVolume);
    }
}
