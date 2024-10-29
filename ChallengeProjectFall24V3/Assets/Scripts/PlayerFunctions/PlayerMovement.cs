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
    [SerializeField] 
    private Vector2 move;
    private bool isGrounded;
    [SerializeField]
    private Vector3 movement;

    //Jackson's Addition
    [SerializeField]
    private Vector3 outsideVelocity;
    private bool ignoreGround;
    [SerializeField] private Vector3 storedMomentum;
    [SerializeField] private bool isMoved;
    [Header("Jackson's Variables")]
    [SerializeField] private float outsideVelocityDeceleration;
    [SerializeField] private float outsideAirDeceleration;
    [SerializeField] private float storedMomentumTimer;
    private float timer;

    private void Awake()
    {
        //initalize private variables
        controller = GetComponent<CharacterController>();
        input = new MainInput();

        //Jackson's variables
        outsideVelocity = new Vector3(0, 0, 0);
        ignoreGround = false;
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
        VerticalMovement();
        GroundCheck();
        OutsideMovement();
    }

    private void LateUpdate()
    {
        LateOutsideMovement();
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

        movement = transform.right * xDir + transform.forward * zDir;
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

    //  ------------------------Jackson's Methods------------------------------
    public void AddOutsideVelocity(Vector3 velocity)
    {
        outsideVelocity += velocity;
    }

    public void SetOutsideVelocity(Vector3 velocity, bool storeMomentum)
    {
        outsideVelocity = velocity;
        isMoved = true;

        if (storeMomentum)
        {
            storedMomentum = velocity;
            timer = storedMomentumTimer;
            ignoreGround = true;
        }
    }


    private void OutsideMovement()
    {
        isMoved = false;

        controller.Move(outsideVelocity * Time.deltaTime);


        if (timer <= 0 && storedMomentum != Vector3.zero)
        {
            storedMomentum = Vector3.zero;
            ignoreGround = false;
        }
        else if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    private void LateOutsideMovement()
    {
        if (isGrounded && input.Ground.Jump.triggered && storedMomentum != Vector3.zero)
        {
            outsideVelocity = storedMomentum;
            Jump();
        }

        if (!isMoved && isGrounded && !gameObject.GetComponent<PlayerSlide>().IsSliding())
        {
            float newXVelocity = Mathf.MoveTowards(outsideVelocity.x, 0, outsideVelocityDeceleration * Time.deltaTime);
            float newYVelocity = outsideVelocity.y;
            if (!ignoreGround)
                newYVelocity = 0;
            float newZVelocity = Mathf.MoveTowards(outsideVelocity.z, 0, outsideVelocityDeceleration * Time.deltaTime);

            outsideVelocity = new Vector3(newXVelocity, newYVelocity, newZVelocity);
        }

        
        else if(!isMoved && !isGrounded)
        {
            float newXVelocity = outsideVelocity.x;
            float newZVelocity = outsideVelocity.z;

            if(newXVelocity * movement.x < 0)
            {
                //Decelerate in air X
                Debug.Log("X Decelerate");
                newXVelocity = Mathf.MoveTowards(newXVelocity, 0, outsideAirDeceleration * Time.deltaTime * Mathf.Abs(movement.x));
            }

            if(newZVelocity * movement.y < 0)
            {
                Debug.Log("Z Decelerate");
                newZVelocity = Mathf.MoveTowards(newZVelocity, 0, outsideAirDeceleration * Time.deltaTime * Mathf.Abs(movement.z));
            }

            outsideVelocity = new Vector3(newXVelocity, outsideVelocity.y, newZVelocity);
        }
        
    }


}
