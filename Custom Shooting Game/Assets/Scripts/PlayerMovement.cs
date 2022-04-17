using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Shooter shooter;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shooter = GetComponent<Shooter>();
    }

    void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        float moveX = transform.position.x + playerInput.moveX * GameManager.instance.player_Speed * Time.deltaTime;
        float moveY = transform.position.y + playerInput.moveY * GameManager.instance.player_Speed * Time.deltaTime;

        transform.position = new Vector2(moveX, moveY);
    }
    private void Rotate()
    {
        float mPosX = playerInput.mousePos.x - transform.position.x;
        float mPosY = playerInput.mousePos.y - transform.position.y;
        float angle = Mathf.Atan2(mPosY, mPosX) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
