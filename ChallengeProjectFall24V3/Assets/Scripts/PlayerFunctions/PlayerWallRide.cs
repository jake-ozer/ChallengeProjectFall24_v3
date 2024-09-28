using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerWallRide : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float speedModifier;
    [SerializeField] private float wallJumpCooldown;
    public bool wallRiding;
    private MainInput input;
    private bool onWall;
    private float cooldownStart;

    void Awake()
    {
        input = new MainInput();
        cooldownStart = wallJumpCooldown;
    }

    // --------- enable/disbale input when script toggled on/off -------------- 
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    // ------------------------Do not touch this------------------------------

    private void Update()
    {
        wallJumpCooldown -= Time.deltaTime;

        //holding jump button
        if(onWall && input.Ground.Wallride.ReadValue<float>() > 0) {
            wallRiding = true;
        }
        else
        {
            wallRiding = false;
        }

        //release jump button
        if(onWall && input.Ground.Wallride.ReadValue<float>() <= 0 && wallJumpCooldown <= 0)
        {
            playerMovement.Jump();
            wallJumpCooldown = cooldownStart;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            onWall = true;
            playerMovement.speed += speedModifier;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            onWall = false;
            playerMovement.speed -= speedModifier;
        }
    }
}
