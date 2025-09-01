using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{

    [SerializeField] private Vector2 spawnPoint;

    void Awake()
    {
        if (spawnPoint == Vector2.zero)
        {
            spawnPoint = FindObjectOfType<PlayerMovement>().transform.position;
        }
    }

    void Start()
    {
        if (spawnPoint == Vector2.zero)
        {
            spawnPoint = FindObjectOfType<PlayerMovement>().transform.position;
        }
    }

    public Vector2 GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void ResetSpawnPoint()
    {
        spawnPoint = Vector2.zero;
    }
}
