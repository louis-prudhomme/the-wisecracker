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

        riotersContainer = GameObject.FindGameObjectWithTag("RiotersContainer");
        riotersRetreats = GameObject.FindGameObjectsWithTag("RiotersRetreat");
        riotersGoals = GameObject.FindGameObjectsWithTag("RiotersGoal");

        for (int i = 0; i < numberOfRioters; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(meshRenderer.bounds.min.x, meshRenderer.bounds.max.x), 
                2,
                Random.Range(meshRenderer.bounds.min.z, meshRenderer.bounds.max.z));

            var rioterController = rioterPrefab.GetComponent<RioterController>();

            rioterController.goal = riotersGoals[Random.Range(0, 3)];
            rioterController.retreat = riotersRetreats[Random.Range(0, 3)];

            Instantiate(rioterPrefab, 
                spawnPosition, 
                rioterPrefab.transform.rotation, 
                riotersContainer.transform)
                .SetActive(true);
        }
    }
}
