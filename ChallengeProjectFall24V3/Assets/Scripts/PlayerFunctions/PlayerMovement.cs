using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public AudioClip jumpSFX;

    //Jackson's Addition
    [SerializeField] private Vector3 outsideVelocity;
    private bool ignoreGround;
    private Vector3 storedMomentum;
    private bool isMoved;
    [Header("Jackson's Variables")]
    [SerializeField] private float outsideVelocityDeceleration;
    [SerializeField] private float outsideAirDeceleration;
    [SerializeField] private float storedMomentumTimer;
    [SerializeField] private float fovChangeSpeed;
    [SerializeField] private Camera myCam;
    private float timer;
    bool jumping;
    private float startingSpeed;

    private float targetFOV;

    private SpeedLineManager speedLines;
    public AudioSource jumpSoundSource;


    private void Awake()
    {
        //initalize private variables
        controller = GetComponent<CharacterController>();
        input = new MainInput();

        //Jackson's variables
        outsideVelocity = new Vector3(0, 0, 0);
        ignoreGround = false;
        jumping = false;
        startingSpeed = speed;
        targetFOV = 60;
        speedLines = transform.GetChild(0).GetChild(3).GetComponent<SpeedLineManager>();

        input.Ground.Jump.performed += JumpPerformed;
        input.Ground.Jump.canceled += JumpCanceled;
    }

    // --------------  tracks if jump is being held -------------- 
    private void JumpPerformed(InputAction.CallbackContext context)
    {
        jumping = true;
    }

    private void JumpCanceled(InputAction.CallbackContext context)
    {
        jumping = false;
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

        //Speed lines
        if (outsideVelocity.x + outsideVelocity.z > 50)
        {
            speedLines.setTrigger(0, true);
        }
        else
        {
            speedLines.setTrigger(0, false);
        }

    }

    private void LateUpdate()
    {
        LateOutsideMovement();

        //Changes player FOV
        myCam.fieldOfView = Mathf.Lerp(myCam.fieldOfView, targetFOV, fovChangeSpeed);

    }


    public void updateSpeed(float speed)
    {
        if(speed == -1)
        {
            this.speed = startingSpeed;
        }
        else
        {
            this.speed += speed;
        }
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
        if(isGrounded && input.Ground.Jump.triggered && outsideVelocity.y == 0)
        {
            Jump(false);
        }

    }

    //used by wallride to give a jump boost when exiting wall ride
    public void Jump(bool playSFX)
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        if(playSFX)
        {
            //stop all sound effects before it (sliding), then play jump
            GetComponent<AudioSource>().Stop();
            jumpSoundSource.PlayOneShot(jumpSFX);
        }
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

    /*
     * Returns movement Vector
     */
    public Vector3 GetMovement()
    {
        return movement;
    }

    /*
     * Adds Velocty
     */
    public void AddOutsideVelocity(Vector3 velocity)
    {
        outsideVelocity += velocity;
    }

    /*
     * Sets outside velocity
     */
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

    /*
     * Returns outside velocity
     */
    public Vector3 GetOutsideVelocity()
    {
        return outsideVelocity;
    }


    private void OutsideMovement()
    {
        isMoved = false;

        //Handles momentum that is stored and then used when player jumps
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
        //Releases stored momentum when player jumps
        if (isGrounded && input.Ground.Jump.triggered && storedMomentum != Vector3.zero)
        {
            outsideVelocity = storedMomentum;
            Jump(false);
        }

        //Slow player down while on the ground unlesss they are sliding
        if (!isMoved && isGrounded && !(gameObject.GetComponent<PlayerSlide>().IsSliding() || gameObject.GetComponent<PlayerSlide>().isTryingToSlide()))
        {
            //Consistent deceleration rate while player is grounded
            float newXVelocity = Mathf.MoveTowards(outsideVelocity.x, 0, outsideVelocityDeceleration * Time.deltaTime);
            float newYVelocity = outsideVelocity.y;
            if (!ignoreGround)
                newYVelocity = 0;
            float newZVelocity = Mathf.MoveTowards(outsideVelocity.z, 0, outsideVelocityDeceleration * Time.deltaTime);

            outsideVelocity = new Vector3(newXVelocity, newYVelocity, newZVelocity);


            //If player slows down their outside velocity decreases as well
            Vector3 totalVelocity = outsideVelocity + movement * speed;
            if(Mathf.Abs(totalVelocity.x) < Mathf.Abs(outsideVelocity.x))
            {
                outsideVelocity = new Vector3(totalVelocity.x, outsideVelocity.y, outsideVelocity.z);
            }
            if(Mathf.Abs(totalVelocity.z) < Mathf.Abs(outsideVelocity.z))
            {
                outsideVelocity = new Vector3(outsideVelocity.x, outsideVelocity.y, totalVelocity.z);
            }
        }

        //Slow player while they are in the air unless they are holding jump
        else if(!isMoved && !isGrounded && !jumping)
        {
            float newXVelocity = outsideVelocity.x;
            float newZVelocity = outsideVelocity.z;

            if(newXVelocity * movement.x < 0)
            {
                newXVelocity = Mathf.MoveTowards(newXVelocity, 0, outsideAirDeceleration * Time.deltaTime * Mathf.Abs(movement.x));
            }

            if(newZVelocity * movement.y < 0)
            {
                newZVelocity = Mathf.MoveTowards(newZVelocity, 0, outsideAirDeceleration * Time.deltaTime * Mathf.Abs(movement.z));
            }

            outsideVelocity = new Vector3(newXVelocity, outsideVelocity.y, newZVelocity);
        }

        //Moves the player (finally)
        controller.Move(outsideVelocity * Time.deltaTime);

    }

    public void updateTargetFOV(float targetFOV)
    {
        if(targetFOV > 0)
        {
            this.targetFOV += targetFOV;
        }
        else
        {
            this.targetFOV = 60;
        }
    }
}
