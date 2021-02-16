using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    private static GameObject player;

    public GameObject radius;

    private Rigidbody body;

    private float baseVelocityForward = 15f;
    private float baseVelocityUp = 5f;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        

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

    private void ComputeParabolic()
    {
        float launchAngle = 45f;
        Vector3 mousePosition = Utils.MousePosition();

        float R = Vector3.Distance(new Vector3(transform.position.x, 0.0f, transform.position.z),
            new Vector3(mousePosition.x, 0.0f, mousePosition.z));

        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(launchAngle * Mathf.Deg2Rad);
        float H = transform.position.y;

        print($"R = {R}");
        print($"G = {G}");
        print($"H = {H}");
        print($"Vz = {G * R * R} / {(2f * (H - R * tanAlpha))}");
        print($"Vz = {G * R * R} / (2 * {(H - R * tanAlpha)})");
        print($"Vz = {G * R * R / (2f * (H - R * tanAlpha))}");

        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;
        Vector3 globalVelocity = transform.TransformDirection(new Vector3(0f, Vy, Vz));
    }
}
