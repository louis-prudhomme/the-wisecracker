using UnityEngine;
using UnityEngine.AI;

public class RioterController : MonoBehaviour
{
    private RiotersStats stats;
    private GameObject player;

    public GameObject normal;
    public GameObject passedOut;
    
    public GameObject goal;
    public GameObject retreat;

    private NavMeshAgent agent;

    private float lastAttack;

    private bool stateHandled = true;
    private RioterState state = RioterState.STANDARD;

    void Start()
    {
        player = Utils.FindGameObject(Utils.Tags.PLAYER);

        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.transform.position;

        stats = GetComponent<RiotersStats>();
        Anger = stats.angerMinimum;
        Fear = stats.fearMinimum;
    }

    private void Update()
    {
        UpdateState();
        UpdateStats();
        UpdateGoal();
    }

    private void UpdateState()
    {
        if (state == RioterState.PASSED_OUT)
            return;

        if (IsAngry)
            if (Fear <= Anger)
                State = RioterState.ANGRY;
            else
                State = RioterState.AFRAID;
        else if (IsAfraid)
            State = RioterState.AFRAID;
        else if (IsScared)
            State = RioterState.FLEEING;
        else
            State = RioterState.STANDARD;
    }

    private void UpdateStats()
    {
        if (lastAttack < stats.attackDelay)
            lastAttack += Time.deltaTime;

        agent.speed = stats.baseSpeed
            + stats.SpeedIncrement(AngerPercentage);
        agent.stoppingDistance = state == RioterState.ANGRY
            ? stats.angryStoppingDistance
            : stats.baseStoppingDistance;

        FearSurge -= stats.surgeDecrease * Time.deltaTime;
        AngerSurge -= stats.surgeDecrease * Time.deltaTime;

        AutoAssess();
    }

    private void AutoAssess()
    {
        if (IsAfraid || State == RioterState.PASSED_OUT)
            return;
        else if (IsAngry)
            Fear -= stats.fearContagion
                * Time.deltaTime
                * 2;
        else
            Fear -= stats.fearContagion
                * Time.deltaTime;
    }

    private void UpdateGoal()
    {
        if (state == RioterState.ANGRY)
            agent.destination = player.transform.position;
        if (stateHandled)
            return;

        switch (state)
        {
            case RioterState.AFRAID:
                agent.destination = retreat.transform.position;
                break;
            case RioterState.STANDARD:
                agent.destination = goal.transform.position;
                break;
            case RioterState.FLEEING:
                agent.destination = ComputeEscapePosition(player);
                break;
            case RioterState.PASSED_OUT:
                PassOut();
                break;
        }
        stateHandled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Utils.Tags.PLAYER)
            && state != RioterState.ANGRY
            && state != RioterState.PASSED_OUT)
            Fear = IsScared
                ? Fear
                : stats.fleeCap + 5;
        else if (state == RioterState.ANGRY)
            Attack();
    }

    private void Attack()
    {
        if (CanAttack && IsPlayerNear)
            player.GetComponent<PlayerController>()
                .DoDamage(stats.attackStrenght);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Utils.Tags.RIOTER))
            AssessComrade(other.gameObject
                .GetComponent<RioterController>());
    }

    private void AssessComrade(RioterController other)
    {
        if (other.State == RioterState.PASSED_OUT)
            return;

        if (other.State == RioterState.STANDARD)
            Fear -= stats.calmContagion
                * Time.deltaTime;
        if (other.State == RioterState.AFRAID)
            DirectFear += stats.fearContagion
                * Time.deltaTime;
        if (other.State == RioterState.FLEEING)
            DirectFear += stats.fearContagion
                * Time.deltaTime
                / 2;

        if (other.IsAngry)
        {
            Anger += stats.angerContagion
                * Time.deltaTime;
            Fear -= stats.fearContagion
                * Time.deltaTime
                * 2;
        }
    }

    private Vector3 ComputeEscapePosition(GameObject player)
    {
        return (transform.position - player.transform.position)
            .normalized
            * (stats.fleeDistance 
                - stats.fleeDistance * AngerPercentage)
            + transform.position;
    }

    public void Scare(ScareType type)
    {
        switch(type)
        {
            case ScareType.GRENADE:
                Fear += stats.fearCap;
                break;
            case ScareType.VICTIM:
                Fear += stats.fearCap;
                Anger += stats.angerAmountVictim;
                break;
            case ScareType.SHOTGUN:
                Fear += stats.fleeCap;
                break;
        }
    }

    private void PassOut()
    {
        agent.enabled = false;

        normal.SetActive(false);
        passedOut.SetActive(true);

        GetComponent<CapsuleCollider>().isTrigger = true;
    }

    public void KnockOut()
    {
        State = RioterState.PASSED_OUT;
        foreach (var c in Physics.OverlapSphere(transform.position, stats.passingOutScareRadius))
            if (c.gameObject.CompareTag(Utils.Tags.PLAYER))
                print("close one"); //todo buff ? galvanisation ?
            else if (c.gameObject.CompareTag(Utils.Tags.RIOTER))
                c.GetComponent<RioterController>()
                    .Scare(ScareType.VICTIM);
    }

    private bool CanAttack => stats.attackDelay <= lastAttack;
    private bool IsPlayerNear => stats.attackRange
        > Vector3.Distance(transform.position, player.transform.position);
    private float AngerPercentage => currentAnger / stats.angerMaximum;

    private bool IsAngry => Anger > stats.angerCap;
    private bool IsAfraid => Fear > stats.fearCap;
    private bool IsScared => Fear > stats.fleeCap;

    public float currentFear;
    public float currentAnger;

    public float currentFearSurge;
    public float currentAngerSurge;

    private float DirectFear
    {
        get => currentFear;
        set
        {
            currentFear = value;
            if (currentFear < stats.fearMinimum)
                currentFear = stats.fearMinimum;

            if (currentFear > stats.fearMaximum)
                currentFear = stats.fearMaximum;
        }
    }

    private float Fear
    {
        get => currentFear + currentFearSurge;
        set
        {
            currentFear = value;
            if (currentFear < stats.fearMinimum)
                currentFear = stats.fearMinimum;

            if (currentFear <= stats.fearMaximum)
                return;

            FearSurge += currentFear - stats.fearMaximum;
            currentFear = stats.fearMaximum;
        }
    }

    private float Anger
    {
        get => currentAnger + currentAngerSurge;
        set
        {
            currentAnger = value;
            if (currentAnger < stats.angerMinimum)
                currentAnger = stats.angerMinimum;

            if (currentAnger <= stats.angerMaximum)
                return;

            AngerSurge += currentAnger - stats.angerMaximum;
            currentAnger = stats.angerMaximum;
        }
    }

    private float AngerSurge
    {
        get => currentAngerSurge;
        set
        {
            currentAngerSurge = value;
            if (currentAngerSurge > stats.surgeMaximum)
                currentAngerSurge = stats.surgeMaximum;

            if (currentAngerSurge < stats.surgeMinimum)
                currentAngerSurge = stats.surgeMinimum;
        }
    }
    private float FearSurge
    {
        get => currentFearSurge;
        set
        {
            currentFearSurge = value;

            if (currentFearSurge > stats.surgeMaximum)
                currentFearSurge = stats.surgeMaximum;

            if (currentFearSurge < stats.surgeMinimum)
                currentFearSurge = stats.surgeMinimum;
        }
    }

    public RioterState State
    {
        get => state;
        set
        {
            stateHandled = false;
            state = value;
        }
    }
}
