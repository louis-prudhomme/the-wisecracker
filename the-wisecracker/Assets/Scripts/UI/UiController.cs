using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public Canvas hud;
    public StateScreenController stateScreen;

    private GameController game;
    private PlayerController player;

    void Start()
    {
        game = Utils.FindGameObject(Utils.Tags.GAME)
            .GetComponent<GameController>();
        player = Utils.FindGameObject(Utils.Tags.PLAYER)
            .GetComponent<PlayerController>();

        game.DefeatEvent = OnDefeat;
        game.VictoryEvent = OnVictory;
        player.DeathEvent = OnPlayerDeath;

        stateScreen.Summon(StateScreenController.ScreenState.OFF);

        hud.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape")
            && stateScreen.IsEscapeAllowed)
            TogglePause();
    }

    private void OnPlayerDeath()
    {
        hud.gameObject.SetActive(false);
        stateScreen.Summon(StateScreenController.ScreenState.DEATH);
    }

    private void OnVictory()
    {
        hud.gameObject.SetActive(false);
        stateScreen.Summon(StateScreenController.ScreenState.VICTORY);
    }

    private void OnDefeat()
    {
        hud.gameObject.SetActive(false);
        stateScreen.Summon(StateScreenController.ScreenState.DEFEAT);
    }

    public void TogglePause()
    {
        hud.gameObject.SetActive(!hud.gameObject.activeSelf);

        stateScreen.Summon(hud.gameObject.activeSelf
            ? StateScreenController.ScreenState.OFF
            : StateScreenController.ScreenState.PAUSED);
    }
}
