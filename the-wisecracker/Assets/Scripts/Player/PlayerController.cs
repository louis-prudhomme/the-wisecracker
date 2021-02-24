using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStats stats;
    private CharacterController controller;
    private WeaponController weaponController;

    public Action HurtEvent;
    public Action DeathEvent;

    private Animator animator;

    private float hp;

    private Vector3 position = new Vector3();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        weaponController = GetComponent<WeaponController>();
     
        animator = GetComponent<Animator>();

        hp = Stats.baseHp;
    }

    void Update()
    {
        if (Stats.frozen)
            return;

        MovePlayer();
        RotatePlayer();
        HandleShooting();
    }

    public void Freeze() { Stats.frozen = true; }
    public void Unfreeze() { Stats.frozen = false; }

    public void DoDamage(float amount)
    {
        hp -= amount;
        HurtEvent();
        if (hp < 0)
            DeathEvent();
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
        Stats.moving = 0 <
            Mathf.Abs(Input.GetAxis("Vertical")) +
            Mathf.Abs(Input.GetAxis("Horizontal"));

        if (Stats.moving) animator.Play("Moving");
        else animator.Play("Idle");

        position = (transform.TransformDirection(Vector3.forward)
            * Stats.positionSpeed
            * Input.GetAxis("Vertical"))
            + (transform.TransformDirection(Vector3.right)
            * Stats.positionSpeed
            * Input.GetAxis("Horizontal"));

        if (!controller.isGrounded) position.y -= Stats.gravity;
        position *= Time.deltaTime;

        controller.Move(position);
    }

    public float Hp => hp;
    private PlayerStats Stats
    {
        get
        {
            if (stats == null)
                stats = GetComponent<PlayerStats>();
            return stats;
        }
    }
}
