using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameIndicatorsController : MonoBehaviour
{
    public Slider timerIndicator;
    public Slider doorIndicator;
    public Text rioterIndicator;

    private GameController game;

    void Start()
    {
        game = Utils.FindGameObject(Utils.Tags.GAME)
            .GetComponent<GameController>();
    }

    void Update()
    {
        timerIndicator.value = game.ArmyTimer;
        doorIndicator.value = game.DoorDurability;
        rioterIndicator.text = game.NumberRioters.ToString();
    }
}
