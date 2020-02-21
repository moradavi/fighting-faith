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

    void Start()
    {
        LoadEnemyData();
    }

    //Assign enemy data
    void LoadEnemyData()
    {
        spriteRenderer.sprite = enemyData.sprite;
        regularSpeed = enemyData.regularSpeed;
        enragedSpeed = enemyData.enragedSpeed;
        health.maxHealth = enemyData.maxHP;
        waypointMovement.waypointPauseTime = enemyData.waypointPauseTime;
    }

    void Update()
    {
        //Assign speed
        EnemyMovement();
    }


    //Enemy speed changes after its health is reduced to 1/3 of maximum
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