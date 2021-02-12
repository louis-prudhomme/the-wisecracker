using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public GameObject radius;

    private Rigidbody body;

    private float baseVelocityForward = 12f;
    private float baseVelocityUp = 10f;

    private void Start()
    {
        body = GetComponent<Rigidbody>();

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
        if (collision.gameObject.tag == "Player")
            return;

        GetComponentInChildren<MeshRenderer>().enabled = false;
        body.isKinematic = true;

        Instantiate(radius, transform.position, transform.rotation, transform.parent).SetActive(true);
        Destroy(gameObject);
    }
}
