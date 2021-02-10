using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStats stats;
    private CharacterController controller;

    private Animator animator;

    public Vector3 position = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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

        if (stats.moving) animator.Play("Moving");
        else animator.Play("Idle");

        position = (transform.TransformDirection(Vector3.forward)
            * stats.positionSpeed
            * Input.GetAxis("Vertical"))
            + (transform.TransformDirection(Vector3.right)
            * stats.positionSpeed
            * Input.GetAxis("Horizontal"));

        transform.rotation *= Quaternion.Euler(0, 
            Input.GetAxis("Horizontal") * stats.rotationSpeed, 0);

        if (!controller.isGrounded) position.y -= stats.gravity;
        position *= Time.deltaTime;

        controller.Move(position);
    }
}
