using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    [Header("Weapon")]
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField][Range(0, 1)] float deathSFXVolume = 0.15f;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField][Range(0, 1)] float jumpSFXVolume = 0.15f;

    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    CircleCollider2D playerFeetCollider;
    float gravityScaleAtStart;
    public bool isAlive = true;
    Vector2 deathKick = new(10f, 10f);
    GameSession gameSession;

    void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<CircleCollider2D>();
        gravityScaleAtStart = playerRigidbody.gravityScale;
    }


    void Update()
    {
        if (!isAlive) { return; }
        Run();
        ClimbLadder();
        GameOver();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        if (gameSession.GetCurrentMana() > 0)
        {
            gameSession.UseMana(1);
            GameObject newArrow = Instantiate(arrow, bow.position, transform.rotation);
            newArrow.GetComponent<Arrow>().PlayFireSFX();
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!IsAllowedToJump())
        {
            return;
        }

        if (value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
            PlayJumpSFX();
        }
    }

    bool IsAllowedToJump()
    {
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return true;
        }
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("DynamicObject")))
        {
            return true;
        }
        return false;
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
        FlipSprite();
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }


    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerRigidbody.gravityScale = gravityScaleAtStart;
            playerAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
        playerRigidbody.velocity = climbVelocity;
        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        playerRigidbody.gravityScale = 0f;
    }

    void GameOver()
    {
        if (IsDyingTouch())
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerRigidbody.velocity = deathKick;
            gameSession.ProcessPlayerDeath();
            PlayDieSFX();
        }
    }
    bool IsDyingTouch()
    {
        if (IsTouchingEnemy()) { return true; }
        if (IsTouchingHazard()) { return true; }

        return false;
    }
    bool IsTouchingEnemy()
    {
        return playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"));
    }
    bool IsTouchingHazard()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard"))) { return true; }
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Hazard"))) { return true; }

        return false;
    }

    void PlayDieSFX()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
    void PlayJumpSFX()
    {
        AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position, jumpSFXVolume);
    }
}
