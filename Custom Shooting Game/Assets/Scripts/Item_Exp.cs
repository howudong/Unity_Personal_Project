using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Exp : MonoBehaviour,Item
{
    private int expPoint;
    private SpriteRenderer expSprite;

    private void Awake()
    {
        expSprite = GetComponent<SpriteRenderer>();
    }
    public void Use(GameObject target)
    {
        PlayerHealth player = target.GetComponent<PlayerHealth>();
        if (player != null)
        {
            UIManager.instance.UpdateExpSlider(expPoint);
        }
        Destroy(gameObject);

    }
    public void SetUp(int newPoint)
    {
        expPoint = newPoint;
    }
}
