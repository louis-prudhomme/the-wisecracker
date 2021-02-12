using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public GameObject flash;
    private Rigidbody body;
    private static GameObject player;

    private float baseVelocityForward = 12f;
    private float baseVelocityUp = 10f;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        body.AddForce(Utils.MousePosition(transform.position.y) - transform.position, 
            ForceMode.Impulse);
        body.velocity = transform.up * baseVelocityUp
            + transform.forward * baseVelocityForward;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(player))
            return;

        GetComponentInChildren<MeshRenderer>().enabled = false;
        body.isKinematic = true;

        Instantiate(flash, transform.position, transform.rotation).SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("tamer");
    }
}
