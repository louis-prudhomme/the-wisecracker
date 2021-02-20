using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public Canvas hud;
    public StateScreenController stateScreen;
    
    void Start()
    {
        hud.gameObject.SetActive(true);
        stateScreen.Summon(StateScreenController.ScreenState.OFF);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            TogglePause();
    }

    public void TogglePause()
    {
        hud.gameObject.SetActive(!hud.gameObject.activeSelf);

        stateScreen.Summon(hud.gameObject.activeSelf
            ? StateScreenController.ScreenState.OFF
            : StateScreenController.ScreenState.PAUSED);
    }
}
