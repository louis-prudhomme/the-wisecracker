using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    private static GameObject player;

    public GameObject radius;
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        body.velocity = Utils.ComputeParabolic(transform.position, 
            Utils.MousePosition()); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        GetComponentInChildren<MeshRenderer>().enabled = false;
        body.isKinematic = true;

        Instantiate(radius, 
            transform.position, 
            transform.rotation, 
            transform.parent)
            .SetActive(true);
        Destroy(gameObject);
    }
}
