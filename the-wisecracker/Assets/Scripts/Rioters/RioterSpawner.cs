using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RioterSpawner : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public int numberOfRioters = 100;

    public GameObject rioterPrefab;
    private GameObject riotersContainer;

    public GameObject[] riotersGoals;
    public GameObject[] riotersRetreats;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        riotersContainer = Utils.FindGameObject(Utils.Tags.RIOTERS_CONTAINER);

        riotersGoals = GameObject.FindGameObjectsWithTag(Utils.Tags.RIOTERS_GOAL);

        if (riotersRetreats == null || riotersRetreats.Length == 0)
            riotersRetreats = GameObject.FindGameObjectsWithTag(Utils.Tags.RIOTERS_RETREAT);

        for (int i = 0; i < numberOfRioters; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(meshRenderer.bounds.min.x, meshRenderer.bounds.max.x), 
                2,
                Random.Range(meshRenderer.bounds.min.z, meshRenderer.bounds.max.z));

            var rioterController = rioterPrefab.GetComponent<RioterController>();

            rioterController.goal = riotersGoals[Random.Range(0, riotersGoals.Length)];
            rioterController.retreat = riotersRetreats[Random.Range(0, riotersRetreats.Length)];

            Instantiate(rioterPrefab, 
                spawnPosition, 
                rioterPrefab.transform.rotation, 
                riotersContainer.transform)
                .SetActive(true);
        }
    }
}
