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
        if (state == RioterState.STANDARD)
        {
            currentFear += stats.fear.Step * 10 * Time.deltaTime;
            if (currentFear > stats.fear.Cap)
                agent.destination = retreat.transform.position;
        } else
        {
            agent.destination = goal.transform.position;
        }
    }
}
