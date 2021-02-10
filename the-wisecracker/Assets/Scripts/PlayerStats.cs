using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp = 100.0f;

    public float speed = 5f;
    public float gravity = 10f;

    public bool canRun = true;
    public bool canMove = true;

    public bool moving = false;
    public bool dead = false;

    public bool lockMouse = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
