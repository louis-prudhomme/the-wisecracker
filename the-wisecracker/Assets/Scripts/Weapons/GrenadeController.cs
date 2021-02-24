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
            player = Utils.FindGameObject(Utils.Tags.PLAYER);

        body.velocity = Utils.ComputeParabolic(transform.position, 
            Utils.MousePosition()); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Utils.Tags.PLAYER))
            return;
        else if (collision.gameObject.CompareTag(Utils.Tags.RIOTER))
            collision.gameObject
                .GetComponent<RioterController>()
                .KnockOut();

        Instantiate(radius, 
            transform.position, 
            transform.rotation, 
            transform.parent)
            .SetActive(true);
        Destroy(gameObject);
    }
}
