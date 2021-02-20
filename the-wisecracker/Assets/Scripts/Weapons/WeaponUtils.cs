
public enum WeaponType
{
    GRENADE_LAUNCHER,
    SHOTGUN,
}


[System.Serializable]
public struct Weapon
{
    public int clipAmmo;
    public int stockAmmo;

    public int clipSize;
    public int stockSize;

    public float shotDelay;
    public float reloadDelay;

    public WeaponType type;
    public bool IsClipFull => clipAmmo == clipSize;
    public string StockAmmo => stockAmmo.ToString();
    public bool HasAmmo => clipAmmo > 0;
    public string ClipAmmo => clipAmmo.ToString();
    public string Type => type == WeaponType.GRENADE_LAUNCHER
        ? "Grenade Launcher"
        : "Shotgun";

    public float PercentageAmmoInClip => (float)clipAmmo / clipSize;
    public float PercentageAmmoInStock => (float)stockAmmo / stockSize;
}

public enum AmmoState
{
    CRITICAL,
    CONCERNING,
    STANDARD
}