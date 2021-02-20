using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateScreenController : MonoBehaviour
{
    private PlayerController player;

    public Text title;

    public Button resume;
    public Button startOver;
    public Button quit;

    public UiController uiController;

    private Canvas canvas;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();

        quit.onClick.AddListener(OnButtonQuitClick);
        resume.onClick.AddListener(OnButtonResumeClick);
        startOver.onClick.AddListener(OnButtonStartOverClick);

        uiController = GetComponentInParent<UiController>();
        canvas = GetComponent<Canvas>();
        title.text = "Paused";
    }

    public void Summon(ScreenState state)
    {
        switch (state)
        {
            case ScreenState.PAUSED:
                resume.gameObject.SetActive(true);
                break;
        }
        TogglePause(state == ScreenState.OFF);
    }

    private void TogglePause(bool isScreenDisabled)
    {
        Time.timeScale = isScreenDisabled ? 1f : 0f;
        
        if (!isScreenDisabled)
            player.Freeze();
        else
            player.Unfreeze();

        Canvas().enabled = !isScreenDisabled;
    }

    private void OnButtonQuitClick()
    {
        Application.Quit();
    }

    private void OnButtonResumeClick()
    {
        uiController.TogglePause();
    }

    private void OnButtonStartOverClick()
    {
        uiController.TogglePause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //todo fix camera bug
    }

    public enum ScreenState
    {
        PAUSED,
        OFF,
        WON,
        LOST
    }

    private Canvas Canvas()
    {
        if (canvas == null)
            canvas = GetComponent<Canvas>();
        return canvas;
    }
}
