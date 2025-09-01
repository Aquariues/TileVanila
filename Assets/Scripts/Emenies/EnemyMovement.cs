using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed;
    readonly private List<string> enterFlipTags = new() { "Enemy", "Climbing", "Bounce" };
    readonly private List<string> exitFlipTags = new() { "Ground" };
    Rigidbody2D enemyRigidbody;

    public void Initialize(float speed)
    {
        moveSpeed = speed;
    }

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsFlipWhenExitOnly(other.gameObject.tag))
        {
            FlipEnemyFacing();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsFlipWhenEnterOnly(other.gameObject.tag))
        {
            FlipEnemyFacing();
        }
    }

    void FlipEnemyFacing()
    {
        moveSpeed = -moveSpeed;
        Vector3 localScale = transform.localScale;
        if (localScale.x > 0)
        {
            localScale.x = -1f;
        } else
        {
            localScale.x = 1f;
        }
        transform.localScale = localScale;
    }

    bool IsFlipWhenEnterOnly(string tag)
    {
        return enterFlipTags.Contains(tag);
    }
    
    bool IsFlipWhenExitOnly(string tag)
    {
        return exitFlipTags.Contains(tag);
    }
}
