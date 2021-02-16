using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public Canvas hud;

    // Start is called before the first frame update
    void Start()
    {
        hud.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            TogglePause();
    }

    private void TogglePause()
    {
        hud.gameObject.SetActive(!hud.gameObject.activeSelf);
    }
}
