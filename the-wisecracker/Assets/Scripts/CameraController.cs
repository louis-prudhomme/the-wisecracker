using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerController;

    private float baseDistance = 40f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(
            playerController.transform.position.x,
            baseDistance, 
            playerController.transform.position.z);   
    }
}
