using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtIndicatorController : MonoBehaviour
{
    private float existence = 0f;
    private float duration = 1.5f;

    public float baseAlpha;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        image.color = new Color(255, 255, 255, baseAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        existence += Time.deltaTime;
        image.color = new Color(255, 255, 255, 
            baseAlpha * (1 - existence / duration));

        if (existence > duration)
            Destroy(gameObject);
    }
}
