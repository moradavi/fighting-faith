using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Analytics;

public class Enemy : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public EnemyData enemyData;
    public Health health;
    public WaypointMovement waypointMovement;
    public Collider2D enemyCollider;
    public Animator animator;

    public float regularSpeed;
    public float enragedSpeed;
    public float attackIntervalTime = 15f;
    public bool isAttacking;
    float attackTimer;
    float numAttacksTotal = 0;
    public bool attackCodeEnabled;
    public bool readyToAttack;

    public UnityEvent onAttack;

    void Start()
    {
        LoadEnemyData();
        waypointMovement.onTargetArrive.AddListener(Idle);
        waypointMovement.onTargetLeave.AddListener(Move);
        waypointMovement.onArriveAtAttackWaypoint.AddListener(Attack);
        readyToAttack = false;
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
        if (attackCodeEnabled)
        {
            if ((attackTimer < attackIntervalTime) && (readyToAttack == false))
            {
                attackTimer += Time.deltaTime;
                EnemyMovement();
            }
            else if (isAttacking == false)
            {
                readyToAttack = true; ;
            }
        }
        else
        {
            EnemyMovement();
        }
               
    }

    void Attack()
    {
        isAttacking = true;
        waypointMovement.StopWaypointMovement();
        attackTimer++;
        numAttacksTotal++;
        onAttack.Invoke();
        animator.SetTrigger("Attack");
        //Trigger Attack Animation
    }

    void Idle()
    {
        animator.SetTrigger("Idle");
    }

    private void Move()
    {
        animator.SetTrigger("Move");

        if (readyToAttack)
            waypointMovement.GoToAttackWaypoint();
    }

    public void SendEnemyAnalytics()
    {
        Analytics.CustomEvent("enemyDied", new Dictionary<string, object>
        {
            { "numberOfAttacks", numAttacksTotal }
        });
    }

    public void StopAttacking()
    {
        animator.SetTrigger("Idle");
        Invoke("InvokeStopAttacking", 1f);
    }

    public void InvokeStopAttacking()
    {
        isAttacking = false;
        readyToAttack = false;
        attackTimer = 0;
        waypointMovement.ResumeWaypointMovement();
        waypointMovement.SetNewRandomTargetWaypoint();

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