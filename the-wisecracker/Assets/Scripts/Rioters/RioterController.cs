using UnityEngine;
using UnityEngine.AI;

public class RioterController : MonoBehaviour
{
    private RiotersStats stats;

    public GameObject normal;
    public GameObject passedOut;
    
    public GameObject goal;
    public GameObject retreat;

    private NavMeshAgent agent;

    private double currentFear;
    private double currentAnger;

    private bool stateHandled;

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
            case RioterState.PASSED_OUT:
                if (!stateHandled) PassOut();
                break;
        }
    }

    private void PassOut()
    {
        agent.enabled = false;
        stateHandled = false;
        
        normal.SetActive(false);
        passedOut.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = false;
    }

    private void UpdateState()
    {
        if (state == RioterState.PASSED_OUT)
            return;

        currentFear -= stats.fear.Step * Time.deltaTime;
        if (currentFear > stats.fear.Cap)
            state = RioterState.AFRAID;
        else
            state = RioterState.STANDARD;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("ping");
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
            currentFear = stats.fear.Maximum;
        if (collision.gameObject.tag == "Rioter")
            currentFear = stats.fear.Minimum;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("pong");
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
            currentFear = stats.fear.Maximum;
        if (other.gameObject.tag == "Rioter")
            currentFear = stats.fear.Minimum;
    }

    public void Scare(ScareType type)
    {
        switch(type)
        {
            case ScareType.GRENADE:
                currentFear += stats.fear.Maximum;
                break;
            case ScareType.VICTIM:
                currentFear += stats.fear.Cap;
                break;
            case ScareType.SHOTGUN:
                currentFear += stats.fear.Cap / 2;
                break;
        }
    }

    public void KnockOut()
    {
        state = RioterState.PASSED_OUT;
        foreach (var c in Physics.OverlapSphere(transform.position, stats.passingOutScareRadius))
            if (c.gameObject.tag == "Player")
                print("close one"); //todo buff ? galvanisation ?
            else if (c.gameObject.tag == "Rioter")
                c.GetComponent<RioterController>()
                    .Scare(ScareType.VICTIM);
    }
}
