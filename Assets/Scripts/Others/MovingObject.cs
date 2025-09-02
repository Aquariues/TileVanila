using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 3f;       // Movement speed
    public float moveDistance = 5f; // How far left & right it can move

    private Vector3 startPos;
    private bool movingRight = true;
    readonly private List<string> enterFlipTags = new() { "Ground", "DynamicObject", "Climbing", "Bounce" };

    void Start()
    {
        startPos = transform.position; // Remember start position
    }

    void Update()
    {
        if (movingRight)
        {
            MoveRight();
            if (transform.position.x <= startPos.x - moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            MoveLeft();
            if (transform.position.x >= startPos.x + moveDistance)
            {
                movingRight = true;
            }
        }
    }

    void MoveRight()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    void MoveLeft()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        // If moved too far right → switch direction
        if (transform.position.x >= startPos.x + moveDistance)
        {
            movingRight = false;
        }
    }

    void MoveOppositeDirection()
    {
        movingRight = !movingRight;
    }

    bool IsMoveOppositeWhenCollisionEnter2D(string tag)
    {
        return enterFlipTags.Contains(tag);
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        CollisionEnterWithPlayer(col);

        if (IsMoveOppositeWhenCollisionEnter2D(col.gameObject.tag))
        {
            MoveOppositeDirection();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        CollisionExitWithPlayer(col);
    }

    void CollisionEnterWithPlayer(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(transform);
        }
    }
    
    void CollisionExitWithPlayer(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(null);
        }
    }
    
}
