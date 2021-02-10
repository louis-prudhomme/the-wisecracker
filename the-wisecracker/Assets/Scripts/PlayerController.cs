using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStats stats;
    private CharacterController controller;
    private CameraController playerCamera;

    public Vector3 movement = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = stats.lockMouse 
            ? CursorLockMode.Locked 
            : CursorLockMode.None;

        Cursor.visible = !stats.lockMouse;

        MovePlayer();
    }

    private void MovePlayer()
    {
        stats.moving = stats.canMove && 0 <
            Mathf.Abs(Input.GetAxis("Vertical")) +
            Mathf.Abs(Input.GetAxis("Horizontal"));

        movement = (transform.TransformDirection(Vector3.forward)
            * stats.speed
            * Input.GetAxis("Vertical"))
            + (transform.TransformDirection(Vector3.right)
            * stats.speed
            * Input.GetAxis("Horizontal"));

        if (!controller.isGrounded) movement.y -= stats.gravity;
        movement *= Time.deltaTime;

        controller.Move(movement);
    }
}
