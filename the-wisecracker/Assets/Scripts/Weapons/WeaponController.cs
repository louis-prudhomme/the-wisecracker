using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponStats stats;

    public GameObject grenadePrefab;
    public GameObject shotgunLightPrefab;
    public GameObject shotgunGumPrefab;

    private GameObject projectilesContainer;

    private int currentWeaponIndex = 0;
    private Weapon current;

    private float lastShot;
    private List<Weapon> weapons;

    private bool isReloading = false;
    private float timeSpentReloading = 0;

    private bool isSwaping = false;
    private float timeSpentSwaping = 0;

    private void Start()
    {
        stats = Utils.FindGameObject(Utils.Tags.GAME)
            .GetComponent<WeaponStats>();

        weapons = stats.weapons;
        current = weapons[0];
        lastShot = 0f;

        projectilesContainer = Utils.FindGameObject(Utils.Tags.PROJECTILES_CONTAINER);
    }
    public void Update()
    {
        if (!CanShoot)
            lastShot += Time.deltaTime;

        if (isReloading)
            timeSpentReloading += Time.deltaTime;

        if (isSwaping)
            timeSpentSwaping += Time.deltaTime;

        if (isSwaping && timeSpentSwaping >= stats.swapDelay)
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

    public void Handle(Transform playerTransform)
    {
        if (CanShoot)
        {
            Shoot(playerTransform);
            if (CanShoot)
            {
                isReloading = false;
                current.clipAmmo--;
                lastShot = 0;
            }
            else
            {
                Reload();
            }
        }
        else if (!HasAmmo)
            Reload();
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
    }

    private void Shoot(Transform playerTransform)
    {
        Vector3 instancePosition = Utils.Copy(playerTransform.position)
            + transform.TransformDirection(stats.BarrelOffset);

        switch (current.type)
        {
            case WeaponType.GRENADE_LAUNCHER:
                Instantiate(grenadePrefab,
                    instancePosition,
                    playerTransform.rotation,
                    projectilesContainer.transform)
                    .SetActive(true);
                break;

            case WeaponType.SHOTGUN:
                Instantiate(shotgunGumPrefab,
                    instancePosition,
                    playerTransform.rotation,
                    projectilesContainer.transform)
                    .SetActive(true);
                Instantiate(shotgunLightPrefab,
                    instancePosition,
                    playerTransform.rotation,
                    projectilesContainer.transform)
                    .SetActive(true);
                break;
        }
    }
    public bool CanReload => !current.IsClipFull
        && current.stockAmmo != 0
        && !isSwaping;
    public bool CanShoot => lastShot > current.shotDelay
        && current.HasAmmo
        && !isSwaping;
    public Weapon Current => current;
    public bool CanSwap => !isSwaping;
    public bool HasAmmo => current.HasAmmo;
    public bool IsReloading => isReloading;
    public bool IsSwaping => isSwaping;
    public float PercentageReloaded => timeSpentReloading / current.reloadDelay;

    public AmmoState CurrentWeaponStockState => stats.CurrentWeaponState(current.PercentageAmmoInStock);
    public AmmoState CurrentWeaponClipState => stats.CurrentWeaponState(current.PercentageAmmoInClip);
}
