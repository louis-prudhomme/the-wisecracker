using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponStats : MonoBehaviour
{
    public Vector3 BarrelOffset => Vector3.forward * 2f;

    public List<Weapon> weapons;

    private bool isReloading = false;
    private float timeSpentReloading = 0;

    private bool isSwaping = false;
    private float timeSpentSwaping = 0;
    private float swapDelay = .25f;

    private int currentWeaponIndex = 0;
    private Weapon current;
    private float lastShot;

    private void Start()
    {
        current = weapons[0];
        lastShot = current.shotDelay;
    }

    public void Update()
    {
        if (!CanShoot)
            lastShot += Time.deltaTime;

        if (isReloading)
            timeSpentReloading += Time.deltaTime;

        if (isSwaping)
            timeSpentSwaping += Time.deltaTime;

        if (isSwaping && timeSpentSwaping >= swapDelay)
        {
            isSwaping = false;
            //saving current weapon status
            weapons[currentWeaponIndex] = current;
            //taking new weapon
            currentWeaponIndex = currentWeaponIndex == 0
                ? 1 : 0;
            current = weapons[currentWeaponIndex];
        }

        if (isReloading && timeSpentReloading >= current.reloadDelay)
        {
            current.stockAmmo--;
            current.clipAmmo++;
            timeSpentReloading = 0;
            isReloading = CanReload;
        }
    }

    public void Shoot()
    {
        if (CanShoot)
        {
            isReloading = false;
            current.clipAmmo--;
            lastShot = 0;
        } else
        {
            Reload();
        }
    }

    public void SwapWeapon()
    {
        if (CanSwap)
        {
            isReloading = false;
            isSwaping = true;
            timeSpentSwaping = 0;
        }
    }

    public void Reload() 
    {
        if (CanReload && !isReloading) 
        { 
            isReloading = true;
            timeSpentReloading = 0;
        }
        //todo slider hud
    }

    public bool CanReload => !current.IsClipFull
        && current.stockAmmo != 0
        && !isSwaping;

    public Weapon Current => current;
    public bool IsSwaping => isSwaping;
    public bool IsReloading => isReloading;
    public float PercentageReloaded => timeSpentReloading / current.reloadDelay;
    public float LastShot => lastShot;
    public bool CanShoot => lastShot > current.shotDelay 
        && current.HasAmmo
        && !isSwaping;
    public bool CanSwap => !isSwaping;

    public bool HasAmmo => current.HasAmmo;

    private AmmoState CurrentWeaponState(float percentage) => percentage < .25
        ? AmmoState.CRITICAL
        : percentage < .50
            ? AmmoState.CONCERNING
            : AmmoState.STANDARD;

    public AmmoState CurrentWeaponStockState => CurrentWeaponState(current.PercentageAmmoInStock);
    public AmmoState CurrentWeaponClipState => CurrentWeaponState(current.PercentageAmmoInClip);
}