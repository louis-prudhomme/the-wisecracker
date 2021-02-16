using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStats stats;
    private CharacterController controller;
    private WeaponController weaponController;

    private Animator animator;

    private Vector3 position = new Vector3();

    void Start()
    {
        stats = GetComponent<PlayerStats>();
        controller = GetComponent<CharacterController>();
        weaponController = GetComponent<WeaponController>();
     
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && stats.CanShoot)
        {
            weaponController.Shoot(transform, Utils.MousePosition());

            stats.lastShot = 0;
        }
    }

    private void RotatePlayer()
    {
        transform.LookAt(Utils.MousePosition(transform.position.y));
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

        if (!controller.isGrounded) position.y -= stats.gravity;
        position *= Time.deltaTime;

        controller.Move(position);
    }

    public WeaponController WeaponController => weaponController;
}
