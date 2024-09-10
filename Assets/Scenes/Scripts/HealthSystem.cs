using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int defensive;

    protected virtual void Start()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        health = maxHealth;
    }

    protected virtual void Update()
    {

    }

    public virtual void TakeDamage(int damage)
    {
        int totaldamage = damage - defensive;
        health -= totaldamage;
        if (health <= 0)
        {
            Die();
            health = 0;
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
