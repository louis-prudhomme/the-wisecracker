using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    private float flashDuration = .1f;
    private float existence = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        existence += Time.deltaTime;
        if (existence > flashDuration)
            Destroy(gameObject);
    }
}
