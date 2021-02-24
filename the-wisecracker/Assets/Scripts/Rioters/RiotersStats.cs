using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RioterState
{
    AFRAID,
    ANGRY,
    PASSED_OUT,
    STANDARD,
    FLEEING
}

public enum ScareType
{
    GRENADE,
    VICTIM,
    SHOTGUN
}

public class RiotersStats : MonoBehaviour
{
    public readonly int angerMinimum = 0;
    public readonly int angerCap = 65;
    public readonly int angerMaximum = 100;

    public readonly int angerAmountVictim = 10;

    public readonly int fearMinimum = 0;
    public readonly int fearCap = 75;
    public readonly int fleeCap = 50;
    public readonly int fearMaximum = 100;

    public readonly int fleeContagion = 2;
    public readonly int fearContagion = 3;
    public readonly int calmContagion = 7;
    public readonly int angerContagion = 5;

    public readonly float baseSpeed = 2f;
    public readonly float maxSpeed = 4f;

    public readonly float attackStrenght = 10f;
    public readonly float attackDelay = 4f;
    public readonly float attackRange = 2f;

    public readonly float baseStoppingDistance = 5f;
    public readonly float angryStoppingDistance = 1f;

    public readonly float surgeDecrease = 5f;
    public readonly float surgeMinimum = 0f;
    public readonly float surgeMaximum = 100f;

    public readonly float playerInfluenceDistance = 10f;

    public float SpeedIncrement(float percentage) => (maxSpeed - baseSpeed) 
        * percentage;

    public float passingOutScareRadius = 3f;
    public readonly float fleeDistance = 10f;
}
