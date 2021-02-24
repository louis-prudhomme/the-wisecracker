using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GoalController : MonoBehaviour
{
    private int numberOfRioters;

    public readonly float detectionDelay = .5f;
    private float lastDetection = 0;

    private void Update()
    {
        if (lastDetection >= detectionDelay)
            CheckForRioters();
        else
            lastDetection += Time.deltaTime;
    }

    private void CheckForRioters()
    {
        numberOfRioters = 0;
        foreach (var c in Physics.OverlapSphere(transform.position, transform.localScale.x))
            if (c.CompareTag(Utils.Tags.RIOTER)
                && c.gameObject.GetComponent<RioterController>().State
                    != RioterState.PASSED_OUT)
                numberOfRioters++;
        lastDetection = 0;
    }

    public int Rioters => numberOfRioters;
}
