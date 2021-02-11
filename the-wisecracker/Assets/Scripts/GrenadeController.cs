using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    private float baseVelocityForward = 12f;
    private float baseVelocityUp = 10f;

    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();

        body.AddForce(Utils.MousePosition(transform.position.y) - transform.position, 
            ForceMode.Impulse);
        body.velocity = transform.up * baseVelocityUp
            + transform.forward * baseVelocityForward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        print("tamer");
    }

}
