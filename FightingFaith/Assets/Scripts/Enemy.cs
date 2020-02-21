using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public EnemyData enemyData;
    public Health health;
    public WaypointMovement waypointMovement;
    public Collider2D enemyCollider;

    public float regularSpeed;
    public float enragedSpeed;

    // Start is called before the first frame update
    void Start()
    {
        LoadEnemyData();
    }

    void LoadEnemyData()
    {
        spriteRenderer.sprite = enemyData.sprite;
        regularSpeed = enemyData.regularSpeed;
        enragedSpeed = enemyData.enragedSpeed;
        health.maxHealth = enemyData.maxHP;
        waypointMovement.waypointPauseTime = enemyData.waypointPauseTime;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if (health.CurrentHealth <= health.maxHealth / 3)
        {
            waypointMovement.speed = enragedSpeed;
        } else
        {
            waypointMovement.speed = regularSpeed;
        }
    }
}
