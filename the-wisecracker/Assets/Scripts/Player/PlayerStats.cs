﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp = 100.0f;

    public float positionSpeed = 5f;
    public float rotationSpeed = 1f;
    public float gravity = 10f;

    public bool canRun = true;
    public bool canMove = true;

    public bool moving = false;
    public bool dead = false;

    public bool lockMouse = true;

    private void Start()
    {
        
    }
}
