using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite sprite;

    public Health health;
    public WaypointMovement waypointMovement;
    public Collider2D enemyCollider;

    public float regularSpeed;
    public float enragedSpeed;

    // Start is called before the first frame update
    void Start()
    {

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
