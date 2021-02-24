using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    public Text title;
    public Text description;

    public Button prev;
    public Button next;
    private int storyIndex;

    public Button go;
    public Button quit;

    private Canvas canvas;

    void Start()
    {
        quit.onClick.AddListener(OnButtonQuitClick);
        go.onClick.AddListener(OnButtonGoClick);

        prev.onClick.AddListener(OnButtonPrevClick);
        next.onClick.AddListener(OnButtonNextClick);

        Index = 0;

        SetActive(true);
    }

    public void SetActive(bool isActive)
    {
        Canvas.enabled = isActive;
    }

    private void OnButtonQuitClick()
    {
        Application.Quit();
    }

    private void OnButtonGoClick()
    {
        SceneManager.LoadScene("Level1");
    }

    private void OnButtonNextClick()
    {
        Index++;
    }

    private void OnButtonPrevClick()
    {
        Index--;
    }

    private void SetButtonActive(Button b, bool isActive)
    {
        b.GetComponent<Image>().enabled = isActive;
        b.GetComponentInChildren<Text>().enabled = isActive;
    }

    private int Index
    {
        get => storyIndex;
        set
        {
            storyIndex = value;

            SetButtonActive(prev, storyIndex != 0);
            SetButtonActive(next, storyIndex != story.Count - 1);

            description.text = story[storyIndex];
        }
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

    private readonly List<string> story = new List<string>()
    {
        "You are a cop, but just not any cop. You are the «Wisecracker».",
        "Although you've done this job and lived this life for too long, habits die hard and so you do.",
        "Past memories of your heroic gang-fighting days fade into cheap whisky as you hope every night not to wake up.",
        "This is another night of blindful obeyment to orders, hidden behind your typical veil of cynical ruthlessness.",
        "...",
        "*ring ring*",
        "« Hey. »",
        "« Prevent those rioters from reaching the glorious president’s house until the army comes »",
        "« They seem pretty angry and will not hesitate to rush you if you knock too much of them out »",
        "« Just scare them off with your flashbang grenades and your riot shotgun »",
        "*clic*"
    };
}
