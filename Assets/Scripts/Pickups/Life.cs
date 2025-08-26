using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [Header("Audio Properties")]
    [SerializeField] AudioClip lifePickupSFX;
    [SerializeField] [Range(0, 1)] float lifePickupVolume = 0.15f;
    GameSession gameSession;
    private bool isCollected = false;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            AudioSource.PlayClipAtPoint(lifePickupSFX, Camera.main.transform.position, lifePickupVolume);
            gameSession.AddToLife();
            // optional 
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}