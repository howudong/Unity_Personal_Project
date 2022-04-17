using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Sword : MonoBehaviour
{
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip swingClip;
    [SerializeField] Transform player;
    private Animator swordAnim;
    private float lastAttackTime = 0f;
    private float delay = 1f;
    private SpriteRenderer swordSprite;
    private PolygonCollider2D colli;
    private AudioSource swordAudio;
    

    void Awake()
    {
        swordAnim = GetComponentInParent<Animator>();
        swordSprite = GetComponent<SpriteRenderer>();
        swordAudio = GetComponent<AudioSource>();
        colli = GetComponent<PolygonCollider2D>();
    }
    private void Update()
    {
        if (swordAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
        {
            swordSprite.enabled = false;
            colli.enabled = true;
        }
        else
        {
            swordSprite.enabled = true;
            colli.enabled = false;
        }
    }
    public void Attack()
    {
        while(Time.time >= lastAttackTime + delay)
        {
            lastAttackTime = Time.time;
            swordAnim.SetTrigger("Attack");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if(enemy != null)
        {
            swordAudio.PlayOneShot(hitClip);
            Vector2 knockDir = (collision.transform.position - player.position).normalized;
            enemy.OnDamaged(GameManager.instance.player_Damage * 2, knockDir, 0.5f, 10f);
        }
        else
        {
            swordAudio.PlayOneShot(swingClip);
        }
    }
}
