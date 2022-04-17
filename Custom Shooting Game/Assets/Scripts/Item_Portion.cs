using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Portion : MonoBehaviour,Item
{
    [SerializeField] int healPoint = 30;
    public void Use(GameObject target)
    {
        PlayerHealth player = target.GetComponent<PlayerHealth>();
        if(player != null)
        {
            player.Heal(healPoint);
            UIManager.instance.UpdateHealthText(player.health);
        }
        Destroy(gameObject);
    }
}
