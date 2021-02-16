using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    private PlayerController playerStats;

    public Text ammo;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ammo.text = $"{playerStats.WeaponController.CurrentWeapon.currentAmmo} " +
            $"| {playerStats.WeaponController.CurrentWeapon.stockAmmo}";
    }
}
