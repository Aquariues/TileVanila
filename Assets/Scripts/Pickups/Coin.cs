using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int pointsForCoin = 10;

    [Header("Audio Properties")]
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] [Range(0, 1)] float volume = 0.15f;
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
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position, volume);
            gameSession.AddToScore(pointsForCoin);
            // optional 
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
