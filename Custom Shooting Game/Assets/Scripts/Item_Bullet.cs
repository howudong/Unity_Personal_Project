using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bullet : MonoBehaviour,Item
{
    [SerializeField] int ammoPoint = 30;
    public void Use(GameObject target)
    {
        Gun gun = target.GetComponentInChildren<Gun>();
        if (gun != null) gun.AddAmmoRemain(ammoPoint);
        Destroy(gameObject);
    }
}
