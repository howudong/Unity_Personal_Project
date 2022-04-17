using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingEntity : MonoBehaviour
{
    protected float startHealth = 100;
    public event Action onDeath;
    public float health{ get;protected set; }
    public bool dead { get;protected set; }
    public float damage { get;protected set;}
    public float speed { get; protected set; }
    public bool isHit { get; protected set; }

    public virtual void OnEnable()
    {
        dead = false;
        health = startHealth;
        isHit = false;
    }
    public virtual void OnDamaged(float damage, Vector2 knockDir, float knockTime, float power)
    {
        if (dead) return;

        health -= Mathf.RoundToInt(damage);
        if (health <= 0) Die();
        else StartCoroutine(KnockBack(knockDir, knockTime, power));
    }
    IEnumerator KnockBack(Vector2 knockDir, float knockTime, float power)
    {
        Rigidbody2D targetRigid = GetComponent<Rigidbody2D>();
        if (targetRigid)
        {
            float cntTime = Time.time;
            while (Time.time <= cntTime + knockTime)
            {
                targetRigid.velocity = knockDir * power;
                yield return null;
            }
            targetRigid.velocity = Vector2.zero;
        }
      
    }
    public virtual void Die()
    {
        dead = true;
        if(onDeath != null)
        {
            onDeath();
        }
    }
    public virtual void Heal(int newHealth)
    {
        health += newHealth;
        if (health >= 100) health = 100;
    }
}
