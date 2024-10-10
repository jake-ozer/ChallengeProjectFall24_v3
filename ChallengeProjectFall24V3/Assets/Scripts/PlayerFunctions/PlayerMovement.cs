using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Speed")]
    [Tooltip("The speed at which the player moves in m/s")]
    public float speed;

    [Header("Player Gravity")]
    public float gravity = -9.81f;

    [Header("Player Jump Height")]
    public float jumpHeight;


    public float groundDistance = 0.4f;
    public LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;



    private CharacterController controller;
    private MainInput input;
    private Vector3 velocity;
    private Vector2 move;
    private bool isGrounded;

    private void Awake()
    {
        //initalize private variables
        controller = GetComponent<CharacterController>();
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
        

        //movement logic happens here
        DirectionalMovement();
        if (!FindObjectOfType<PlayerWallRide>().wallRiding)
        {
            VerticalMovement();
            GroundCheck();
        }
    }


    public void updateSpeed(float speed)
    {
        this.speed += speed;
    }

    public float getSpeed()
    {
        return this.speed;
    }

    private void DirectionalMovement()
    {
        move = input.Ground.Move.ReadValue<Vector2>();
        float xDir = move.x;
        float zDir = move.y;

        Vector3 movement = transform.right * xDir + transform.forward * zDir;
        controller.Move(movement * speed * Time.deltaTime);
    }

    private void VerticalMovement()
    {

        //gravity and falling logic
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //jumping logic
        if(isGrounded && input.Ground.Jump.triggered)
        {
            Jump();
        }
    }

    //used by wallride to give a jump boost when exiting wall ride
    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
    }

    public Vector2 getDirectionalVelo()
    { //Returns Directional Velocity
        return this.move;
    }

    public bool onGround()
    {
        return isGrounded;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }


}
