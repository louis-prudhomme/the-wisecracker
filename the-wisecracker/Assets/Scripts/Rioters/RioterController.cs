using UnityEngine;
using UnityEngine.AI;

public class RioterController : MonoBehaviour
{
    private RiotersStats stats;

    public GameObject goal;
    public GameObject retreat;
    private NavMeshAgent agent;

    private double currentFear;
    private double currentAnger;

    private RioterState state = RioterState.STANDARD;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.transform.position;

        stats = GetComponent<RiotersStats>();
        currentAnger = stats.anger.Minimum;
        currentFear = stats.fear.Minimum;
    }

    private void Update()
    {
        UpdateState();
        UpdateGoal();
    }

    private void UpdateGoal()
    {
        switch (state)
        {
            case RioterState.AFRAID:
                agent.destination = retreat.transform.position;
                break;
            case RioterState.STANDARD:
                agent.destination = goal.transform.position;
                break;
        }
    }

    private void UpdateState()
    {
        currentFear -= stats.fear.Step * Time.deltaTime;
        if (currentFear > stats.fear.Cap)
            state = RioterState.AFRAID;
        else
            state = RioterState.STANDARD;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            currentFear = stats.fear.Maximum;
        if (collision.gameObject.tag == "Rioter")
            currentFear = stats.fear.Minimum;
    }

    public void Scare(ScareType type)
    {
        switch(type) 
        {
            case ScareType.GRENADE:
                currentFear = stats.fear.Maximum;
                break;
        }
    }
}
