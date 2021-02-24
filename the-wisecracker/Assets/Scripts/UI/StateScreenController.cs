using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateScreenController : MonoBehaviour
{
    public Text title;

    public Button resume;
    public Button startOver;
    public Button quit;

    public UiController uiController;
    private PlayerController player;

    private Canvas canvas;
    private ScreenState current;

    void Start()
    {
        quit.onClick.AddListener(OnButtonQuitClick);
        resume.onClick.AddListener(OnButtonResumeClick);
        startOver.onClick.AddListener(OnButtonStartOverClick);

        uiController = GetComponentInParent<UiController>();
    }

    public void Summon(ScreenState state)
    {
        current = state;
        switch (state)
        {
            case ScreenState.PAUSED:
                resume.gameObject.SetActive(true);
                title.text = "Paused";
                break;
            case ScreenState.DEATH:
                resume.gameObject.SetActive(false);
                title.text = "You are dead.";
                break;
            case ScreenState.DEFEAT:
                resume.gameObject.SetActive(false);
                title.text = "You have failed.";
                break;
            case ScreenState.VICTORY:
                if (SceneManager.GetActiveScene().name == "Level1")
                {
                    resume.GetComponentInChildren<Text>().text = "Next level";
                    resume.onClick.RemoveAllListeners();
                    resume.onClick.AddListener(() => { SceneManager.LoadScene("Level2"); });
                }
                else if (SceneManager.GetActiveScene().name == "Level2")
                {
                    resume.GetComponentInChildren<Text>().text = "Restart game";
                    resume.onClick.RemoveAllListeners();
                    resume.onClick.AddListener(() => { SceneManager.LoadScene("TitleScreen"); });
                }
                else resume.gameObject.SetActive(false);
                    
                title.text = "You won !";
                break;
        }
        TogglePause(state != ScreenState.OFF);
    }

    private void TogglePause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
        Canvas.enabled = isPaused;

        if (isPaused) Player.Freeze();
        else Player.Unfreeze();
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
        SceneManager.LoadScene(SceneManager
               .GetActiveScene().buildIndex);
    }

    public enum ScreenState
    {
        PAUSED,
        OFF,
        VICTORY,
        DEATH,
        DEFEAT
    }

    private Canvas Canvas
    {
        get
        {
            if (canvas == null)
                canvas = GetComponent<Canvas>();
            return canvas;
        }
    }

    private PlayerController Player
    {
        get
        {
            if (player == null)
                player = Utils.FindGameObject(Utils.Tags.PLAYER)
                    .GetComponent<PlayerController>();
            return player;
        }
    }

    public bool IsEscapeAllowed => current == ScreenState.PAUSED 
        || current == ScreenState.OFF;
}
