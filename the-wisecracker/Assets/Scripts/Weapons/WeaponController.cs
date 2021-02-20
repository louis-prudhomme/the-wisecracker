using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponStats stats;

    public GameObject grenadePrefab;
    public GameObject shotgunLightPrefab;
    public GameObject shotgunGumPrefab;

    private GameObject projectilesContainer;

    private void Start()
    {
        stats = GameObject
            .FindGameObjectWithTag("Database")
            .GetComponent<WeaponStats>();

        projectilesContainer = GameObject
            .FindGameObjectWithTag("ProjectilesContainer");
    }

    public void Handle(Transform playerTransform)
    {
        if (stats.CanShoot)
        {
            Shoot(playerTransform);
            stats.Shoot();
        }
        else if (!stats.HasAmmo)
            stats.Reload();
    }

    public void SwapWeapon()
    {
        stats.SwapWeapon();
    }

    public void Reload()
    {
        stats.Reload();
    }

    private void Shoot(Transform playerTransform)
    {
        Vector3 instancePosition = Utils.Copy(playerTransform.position)
            + transform.TransformDirection(stats.BarrelOffset);

        switch (stats.Current.type)
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


}
