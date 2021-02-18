using UnityEngine;

public class GumShellController : MonoBehaviour
{
    private static GameObject player;

    private Rigidbody body;

    private float baseVelocityUp = 2f;
    private float baseVelocityForward = 20f;

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

        if (collision.gameObject.tag == "Rioter")
            collision.gameObject
                .GetComponent<RioterController>()
                .KnockOut();

        Destroy(gameObject);
    }
}
