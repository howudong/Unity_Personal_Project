using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float moveX
    {
        get;
        private set;
    }
    public float moveY
    {
        get;
        private set;
    }
    public bool fire
    {
        get;
        private set;
    }
    public bool reload
    {
        get;
        private set;
    }
    public Vector2 mousePos
    {
        get;
        private set;
    }
    public bool change
    {
        get;
        private set;
    }

    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        reload = Input.GetButton("Reload");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fire = Input.GetButton("Fire1");
    }
    private void Update()
    {
        change = Input.GetKeyDown(KeyCode.Tab);
    }
}
