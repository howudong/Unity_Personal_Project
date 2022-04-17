using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private enum Weapon
    {
        Sword,
        Gun
    }

    private Weapon weapon;
    [SerializeField] Sword sword;
    [SerializeField] Gun gun;
    private PlayerInput playerInput;
    private AudioSource playerAudio;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAudio = GetComponent<AudioSource>();
        weapon = Weapon.Gun;
        ChangeWeapon(weapon);
    }

    void Update()
    {
        if (weapon == Weapon.Gun)
        {
            if (playerInput.fire) gun.Fire();
            else if (playerInput.reload) gun.Reload();
        }
        else if(weapon == Weapon.Sword)
        {
            if (playerInput.fire) sword.Attack();
        }
        if (playerInput.change && gun.state != Gun.State.Reload)
        {
            switch (weapon)
            {
                case Weapon.Sword:
                    weapon = Weapon.Gun;
                    break;
                case Weapon.Gun:
                    weapon = Weapon.Sword;
                    break;
            }
            ChangeWeapon(weapon);
        }
    }
    private void ChangeWeapon(Weapon weapon)
    {
        playerAudio.PlayOneShot(playerAudio.clip);
        if(weapon == Weapon.Gun)
        {
            gun.gameObject.SetActive(true);
            sword.gameObject.SetActive(false);
        }
        else if(weapon == Weapon.Sword)
        {
            gun.gameObject.SetActive(false);
            sword.gameObject.SetActive(true);
        }
    }
}
