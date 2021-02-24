using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public readonly float baseHp = 100.0f;
    
    public readonly float positionSpeed = 5f;
    public readonly float rotationSpeed = 1f;
    public readonly float gravity = 10f;
    
    public bool frozen = false;
    
    public bool moving = false;
    public bool dead = false;
}
