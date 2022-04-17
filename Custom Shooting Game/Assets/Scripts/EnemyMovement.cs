using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private EnemyHealth enemyHealth;
    private Rigidbody2D enemyRigid;
    private SpriteRenderer enemySprite;
    private float enemySpeed;

    private void Start()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        if(!GameManager.instance.isGameOver)
            target = FindObjectOfType<PlayerInput>().transform;
        enemySpeed = enemyHealth.speed;
        enemySprite = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if (target != null)
        {
            ChasingTarget();
            LookAtTarget();
            FlipSprite();
        }
    }

    private void LookAtTarget()
    {
        Vector2 rPos = target.position - transform.position;
        float angle = Mathf.Atan2(rPos.y, rPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void ChasingTarget()
    {
        Vector2 moveDir = (target.position - transform.position).normalized;
        enemyRigid.velocity = moveDir * enemySpeed;
    }
    private void FlipSprite()
    {
        float zRotation = transform.rotation.eulerAngles.z % 360;
        if (zRotation < 0) zRotation = 360 - zRotation;

        if (270f >= zRotation && zRotation >= 90f) 
        {
            enemySprite.flipY = true;
        }
        else enemySprite.flipY = false;
    }
}
