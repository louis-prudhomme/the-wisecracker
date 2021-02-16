using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponList weaponList;

    private Weapon currentWeapon;

    public GameObject grenadePrefab;
    private GameObject projectilesContainer;

    private void Start()
    {
        weaponList = GameObject
            .FindGameObjectWithTag("Database")
            .GetComponent<WeaponList>();
        
        projectilesContainer = GameObject
            .FindGameObjectWithTag("ProjectilesContainer");
    }

    public void Shoot(Transform playerTransform, Vector3 mousePosition)
    {
        switch (currentWeapon.type)
        {
            case WeaponType.GRENADE_LAUNCHER:
                GameObject grenade = Instantiate(grenadePrefab,
                    Utils.Copy(playerTransform.position, 1.5f), 
                    playerTransform.rotation,
                    projectilesContainer.transform);
                grenade.SetActive(true);
                break;
        }
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

    public Weapon CurrentWeapon => currentWeapon;


}
