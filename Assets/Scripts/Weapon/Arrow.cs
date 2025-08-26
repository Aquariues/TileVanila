using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] ParticleSystem colliderEffect;

    [Header("Audio Properties")]
    [SerializeField] AudioClip hitSFX;
    [SerializeField] [Range(0, 1)] float hitSFXVolume = 0.15f;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0, 1)] float fireSFXVolume = 0.15f;
    float xSpeed;
    Rigidbody2D arrowRigidBody;
    PlayerMovement playerMovement;


    void Awake()
    {
        arrowRigidBody = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void Start()
    {
        xSpeed = playerMovement.transform.localScale.x * speed;
    }

    // Update is called once per frame
    void Update()
    {
        arrowRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnTouchingSomething();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        OnTouchingSomething();
    }

    void OnTouchingSomething()
    {
        if (colliderEffect != null)
        {
            PlayHitSFX();
            colliderEffect.transform.parent = null;
            colliderEffect.Play();
            Destroy(colliderEffect.gameObject, colliderEffect.main.duration);
        }
        Destroy(gameObject);
    }

    public void PlayFireSFX()
    {
        AudioSource.PlayClipAtPoint(fireSFX, transform.position, fireSFXVolume);
    }
    
    public void PlayHitSFX()
    {
        AudioSource.PlayClipAtPoint(hitSFX, transform.position, hitSFXVolume);
    }
}
