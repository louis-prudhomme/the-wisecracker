using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    GRENADE_LAUNCHER,
    SHOTGUN,
}


[System.Serializable]
public struct Weapon
{
    public int currentAmmo;
    public int stockAmmo;

    public int clipSize;
    public int stockSize;

    public WeaponType type;
}

public enum AmmoState
{
    CRITICAL,
    CONCERNING,
    STANDARD
}

public class WeaponList : MonoBehaviour
{
    public List<Weapon> weapons;
}