using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeFlashController : MonoBehaviour
{
    private float duration = .1f;
    private float existence = 0f;
    
    private float radius = 5f;

    public GameObject flashPrefab;
    private GameObject flash;

    private bool isPlayerColliding = false;
    private List<GameObject> riotersColliding;

    private void Start()
    {
        riotersColliding = new List<GameObject>();

        flash = Instantiate(flashPrefab, transform);
        flash.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        existence += Time.deltaTime;
        if (existence > duration)
        {
            FindColliding();
            Destroy(gameObject);
        }
    }

    private void FindColliding()
    {
        foreach (var c in Physics.OverlapSphere(transform.position, radius))
            if (c.gameObject.tag == "Player")
                isPlayerColliding = true;
            else if (c.gameObject.tag == "Rioter")
                c.GetComponent<RioterController>()
                    .Scare(ScareType.GRENADE);
    }
}
