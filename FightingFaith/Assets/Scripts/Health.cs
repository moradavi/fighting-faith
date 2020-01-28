using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    int health;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    void ResetHealth()
    {
        health = maxHealth;
    }

    void LoseHealth(int damage)
    {
        health -= damage;
    }

    void ReplenishHealth(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
