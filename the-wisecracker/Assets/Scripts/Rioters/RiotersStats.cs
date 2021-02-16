using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Level
{
    private readonly int minimum;
    private readonly int cap;
    private readonly int maximum;
    private readonly int step;

    public int Minimum => minimum;
    public int Cap => cap;
    public int Maximum => maximum;
    public int Step => step;

    public Level(int minimum, int cap, int maximum, int step)
    {
        this.minimum = minimum;
        this.cap = cap;
        this.maximum = maximum;
        this.step = step;
    }
}
public enum RioterState
{
    AFRAID,
    ANGRY,
    STANDARD
}

public enum ScareType
{
    GRENADE
}

public class RiotersStats : MonoBehaviour
{
    public Level anger = new Level(0, 50, 100, 1);
    public Level fear = new Level(0, 50, 100, 5);
}
