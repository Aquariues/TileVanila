using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [Header("Mana Properties")]
    [SerializeField] int manaAmount = 10;
    [Header("Audio Properties")]
    [SerializeField] AudioClip manaPickupSFX;
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
            AudioSource.PlayClipAtPoint(manaPickupSFX, Camera.main.transform.position, volume);
            gameSession.AddToMana(manaAmount);
            // optional 
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}