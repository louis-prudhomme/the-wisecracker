using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp = 100.0f;

    public float positionSpeed = 5f;
    public float rotationSpeed = 1f;
    public float gravity = 10f;

    public bool frozen = false;

    public bool moving = false;
    public bool dead = false;
}
