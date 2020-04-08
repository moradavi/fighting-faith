using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public bool IsDead { get; private set; }
    public int CurrentHealth { get; private set; }
    public UnityEvent onDeath = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void LoseHealth(int damage)
    {
        if(damage >= 0)
        {
            ChangeHealthBy(-damage);
            Debug.Log(damage);
        }
            
        else
            Debug.LogWarning("Damage values must be positive");
    }

    public void GainHealth(int heal)
    {
        if (heal >= 0)
            ChangeHealthBy(heal);
        else
            Debug.LogWarning("Heal values must be positive");
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        IsDead = false;
    }

    public void EmptyHealth()
    {
        CurrentHealth = 0;
        Die();
    }

    void ChangeHealthBy(int i)
    {
        CurrentHealth += i;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        if (CurrentHealth <= 0)
            Die();
        else
            IsDead = false;
    }

    void Die()
    {
        onDeath.Invoke();
        IsDead = true;
    }
}
