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
    public bool wallRiding;
    private MainInput input;
    private bool onWall;

    void Awake()
    {
        input = new MainInput();
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
        //control wall ride
        if(onWall && input.Ground.Wallride.ReadValue<float>() > 0) {
            wallRiding = true;
            OnWall();
        }
        else
        {
            wallRiding = false;
        }

        if(onWall && input.Ground.Wallride.ReadValue<float>() <= 0)
        {
            OffWall();
            onWall = false;
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

    private void OnWall()
    {
        Debug.Log("on wall");
        
    }

    private void OffWall()
    {
        Debug.Log("off wall");
        playerMovement.Jump();
        
    }
}
