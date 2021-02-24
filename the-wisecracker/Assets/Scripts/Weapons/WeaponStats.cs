using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponStats : MonoBehaviour
{
    public List<Weapon> weapons;

    public Vector3 BarrelOffset => Vector3.forward * 2f;
    public readonly float swapDelay = .25f;

    public AmmoState CurrentWeaponState(float percentage) => percentage < .25
        ? AmmoState.CRITICAL
        : percentage < .50
            ? AmmoState.CONCERNING
            : AmmoState.STANDARD;
}