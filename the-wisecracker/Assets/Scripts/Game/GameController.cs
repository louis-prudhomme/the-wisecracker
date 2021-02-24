using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private readonly List<GoalController> riotersGoals = new List<GoalController>();
    private GameStats stats;

    private int numberOfRiotersAtGoal;

    private float lastDemolition;
    private float doorDurability;
    private float armyTimer;

    public Action DefeatEvent;
    public Action VictoryEvent;

    void Start()
    {
        stats = GetComponent<GameStats>();

        armyTimer = 0;
        doorDurability = stats.doorBaseDurability;
        foreach (var g in Utils.FindGameObjects(Utils.Tags.RIOTERS_GOAL))
            riotersGoals.Add(g.GetComponent<GoalController>());
    }

    void Update()
    {
        if (armyTimer > stats.timeForArmyToCome
            || doorDurability <= 0)
            return;

        numberOfRiotersAtGoal = 0;
        armyTimer += Time.deltaTime;

        if (armyTimer > stats.timeForArmyToCome)
            VictoryEvent();

        foreach (var goal in riotersGoals)
            numberOfRiotersAtGoal += goal.Rioters;

        if (lastDemolition >= stats.rioterDemolitionDelay)
        {
            lastDemolition = 0;
            doorDurability -= numberOfRiotersAtGoal
                * stats.rioterDemolitionStrenght;
        }
        else lastDemolition += Time.deltaTime;

        if (doorDurability <= 0)
            DefeatEvent();
    }

    public float ArmyTimer => armyTimer / stats.timeForArmyToCome;
    public float DoorDurability => doorDurability / stats.doorBaseDurability;
    public float NumberRioters => numberOfRiotersAtGoal;
}
