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

    void Update()
    {
        if (stats.frozen)
            return;

        MovePlayer();
        RotatePlayer();
        HandleShooting();
    }

    public void Freeze()
    {
        stats.frozen = true;
    }

    public void Unfreeze()
    {
        stats.frozen = false;
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1"))
            weaponController.Handle(transform);
        if (Input.GetButton("Reload"))
            weaponController.Reload();
        if (Input.GetButton("Mouse ScrollWheel") 
            || Input.GetButton("SwapWeapon"))
            weaponController.SwapWeapon();
    }

    private void RotatePlayer()
    {
        transform.LookAt(Utils.MousePosition(transform.position.y));
    }

    private void MovePlayer()
    {
        stats.moving = 0 <
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
}
