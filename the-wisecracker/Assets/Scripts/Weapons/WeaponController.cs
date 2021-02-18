using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponList weaponList;

    private Weapon currentWeapon;
    private float lastShot = float.MaxValue;

    public GameObject grenadePrefab;
    public GameObject shotgunLightPrefab;
    public GameObject shotgunGumPrefab;

    private Vector3 barrelOffset = Vector3.forward * 2f;

    private GameObject projectilesContainer;

    private void Start()
    {
        weaponList = GameObject
            .FindGameObjectWithTag("Database")
            .GetComponent<WeaponList>();
        currentWeapon = weaponList.weapons[1];

        projectilesContainer = GameObject
            .FindGameObjectWithTag("ProjectilesContainer");
    }

    public void Shoot(Transform playerTransform)
    {
        if (CanShoot)
            Handle(playerTransform);
    }

    private void Handle(Transform playerTransform)
    {
        lastShot = 0;
        Vector3 instancePosition = Utils.Copy(playerTransform.position)
            + transform.TransformDirection(barrelOffset);

        switch (currentWeapon.type)
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

    public void Update()
    {
        lastShot += Time.deltaTime;
    }

    private float percentageAmmoInClip => currentWeapon.currentAmmo 
        / currentWeapon.clipSize;
    private float percentageAmmoInStock => (currentWeapon.currentAmmo 
        + currentWeapon.stockAmmo) 
        / currentWeapon.stockSize;

    public AmmoState CurrentWeaponState (float percentage) => percentage < .25
        ? AmmoState.CRITICAL
        : percentage < .50
            ? AmmoState.CONCERNING
            : AmmoState.STANDARD;

    public AmmoState CurrentWeaponStockState => CurrentWeaponState(percentageAmmoInStock);
    public AmmoState CurrentWeaponClipState => CurrentWeaponState(percentageAmmoInClip);

    public bool CanShoot => lastShot > currentWeapon.shotDelay;
    public Weapon CurrentWeapon => currentWeapon;


}
