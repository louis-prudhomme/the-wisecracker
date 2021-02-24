using UnityEngine;

public class GumShellController : MonoBehaviour
{
    private static GameObject player;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (player == null)
            player = Utils.FindGameObject(Utils.Tags.PLAYER);

        body.velocity = Utils.ComputeParabolic(transform.position,
            Utils.MousePosition());
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
