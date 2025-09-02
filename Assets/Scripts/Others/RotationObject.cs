using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] bool isClockwise = true;

    void Update()
    {
        if (isClockwise)
        {
            RotateClockwise();
        }
        else
        {
            RotateCounterClockwise();
        }
    }
    void RotateClockwise()
    {
        Vector3 rotation = new Vector3(0, 0, -speed * Time.deltaTime);
        transform.Rotate(rotation);
    }

    void RotateCounterClockwise()
    {
        Vector3 rotation = new Vector3(0, 0, speed * Time.deltaTime);
        transform.Rotate(rotation);
    }
}
