using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    private Rigidbody2D enemyRigid;
    public float intensity
    {
        get;private set;
    }
    private Animator enemyAnim;

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        enemyRigid = GetComponent<Rigidbody2D>();
    }
    public override void OnDamaged(float damage, Vector2 knockDir, float knockTime, float power)
    {
        base.OnDamaged(damage, knockDir,knockTime,power);


    }

    public void SetUp(float newSpeed, Color newColor, float newDamage, float newHealth, float newIntensity)
    {
        speed = newSpeed;
        damage = newDamage;
        startHealth = newHealth;
        health = newHealth;
        intensity = newIntensity;
        GetComponent<SpriteRenderer>().color = newColor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (player != null)
        {
            enemyAnim.SetTrigger("Attack");
            Vector2 dir = (player.transform.position - transform.position).normalized;
            player.OnDamaged(damage/2, dir,1f,5f);
        }
    }
 
}
