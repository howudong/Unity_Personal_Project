using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private Gun gun;
    private Rigidbody2D playerRigid;
    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        gun = GetComponent<Gun>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        UIManager.instance.UpdateHealthText(health);
    }

    public override void OnDamaged(float damage, Vector2 knockDir,float knockTime, float power)
    {
        if (!isHit)
        {
            base.OnDamaged(damage, knockDir,knockTime, power);
            UIManager.instance.UpdateHealthText(health);
            StartCoroutine(DamageEffect());
        }
    }
    IEnumerator DamageEffect()
    {
        isHit = true;
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        Color color = playerSprite.color;

        gameObject.layer = 9;
        color.a = 0.5f;
        playerSprite.color = color;

        yield return new WaitForSeconds(1f);

        isHit = false;
        gameObject.layer = 0;
        color.a = 1f;
        playerSprite.color = color;
    }
    public override void Die()
    {
        onDeath += () => Destroy(gameObject);
        onDeath += GameManager.instance.EndGame;
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null) item.Use(gameObject);
    }
}
