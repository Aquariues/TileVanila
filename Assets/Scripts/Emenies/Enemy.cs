using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxEnemyHealth = 5;
    [SerializeField] private float moveSpeed = 2f;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    private EnemyMovement movement;
    private EnemyHealth health;
    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
    }

    void Start()
    {
        // Initialize sub-components with Enemy's settings
        health.Initialize(maxEnemyHealth, healthSlider);
        movement.Initialize(moveSpeed);
    }
}
