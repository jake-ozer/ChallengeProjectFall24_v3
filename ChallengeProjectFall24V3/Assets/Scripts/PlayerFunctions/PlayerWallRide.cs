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
    [SerializeField] private float wallRideSpeed;
    public bool wallRiding;
    public LayerMask wallLayer;
    public float wallRiderSize;
    private MainInput input;
    private bool onWall;
    private float cooldownStart;
    private Vector3 movement;
    private bool once = true;

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

            //Get player's velocty in x and z direction (left and right).
            float xDir = playerMovement.getDirectionalVelo().x;
            float zDir = playerMovement.getDirectionalVelo().y;

            if (once)
            {
                //Maintain player's velocity in direction
                if (xDir == 0 && zDir == 0)
                {
                    movement = transform.forward * 1;
                }
                else
                {
                    movement = transform.right * xDir + transform.forward * zDir;
                }
                playerMovement.enabled = false;
                once = false;
            }

            controller.Move(movement * (playerMovement.speed * wallRideSpeed) * Time.deltaTime);
        }
        else
        {
            playerMovement.enabled = true;
            once = true;
        }

        //release jump button
        if(onWall && input.Ground.Wallride.ReadValue<float>() <= 0 && wallRiding)
        {
            playerMovement.Jump();
            wallJumpCooldown = cooldownStart;
            wallRiding = false;
            once = true;
        }

        //just falling off the wall, not jumping off
        if(input.Ground.Wallride.ReadValue<float>() <= 0)
        {
            playerMovement.enabled = true;
        }

        //check if on wall
        onWall = Physics.CheckBox(transform.position, new Vector3(wallRiderSize,1,1), Quaternion.identity, wallLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(wallRiderSize, 1, 1));
    }
    /*    private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Wall")
            {
                onWall = true;
                //playerMovement.speed += speedModifier;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Wall")
            {
                onWall = false;
                //playerMovement.speed -= speedModifier;
                wallRiding = false;
            }
        }*/
}
