using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnCollision : MonoBehaviour
{
    private PlayerWeaponManager playerWeaponManager;

    private void Awake()
    {
        playerWeaponManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerWeaponManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerWeaponManager.GenerateRandomWeapon();
        Destroy(gameObject);
    }
}
