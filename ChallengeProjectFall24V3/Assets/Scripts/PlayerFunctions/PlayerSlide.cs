using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject playerVision;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float slideTime;
    private float prevSpeed;
    private MainInput input;
    private bool sliding = false;
    private bool dirSlide;
    private Vector3 initCamPos;
    [SerializeField] private Vector3 movement;
    public AudioClip slideSFX;
    
    //Jackson's Variables
    private bool tryingToSlide;
    [SerializeField] private bool canBuffer;
    [SerializeField] private float bufferTimer;


    // Start is called before the first frame update
    void Awake()
    {
        input = new MainInput();
        initCamPos = playerVision.transform.localPosition;
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


    // Update is called once per frame
    void Update()
    {

        if(input.Ground.Slide.triggered)
        {
            StartCoroutine("SlideInput");
        }

        if (tryingToSlide && !sliding && playerMovement.onGround()) //make sure player can't slide in air or when already sliding
        {         
            Debug.Log("You pressed slide");
            StartCoroutine("Slide");
            StopCoroutine("SlideInput");
            tryingToSlide = false;
        }


       

        if(sliding && input.Ground.Jump.triggered)
        {
            StopCoroutine("Slide");
            playerMovement.enabled = true;       
            Debug.Log("Movement restored");
            playerVision.transform.localPosition = new Vector3(playerVision.transform.localPosition.x, initCamPos.y, playerVision.transform.localPosition.z);
            playerMovement.Jump(true);
            sliding = false;
            dirSlide = false;
        }


        if(sliding)
        {
            playerVision.transform.Translate(new Vector3(0, -cameraMoveSpeed, 0), Space.World);
            if(dirSlide)
            {
                controller.Move(movement * (playerMovement.speed * slideSpeed) * Time.deltaTime);
            } else
            {
                controller.Move(movement * (playerMovement.speed * slideSpeed) * Time.deltaTime);
            }

        }
    }
    private IEnumerator Slide()
    {
        GetComponent<AudioSource>().PlayOneShot(slideSFX);

        //Outside movement stuff
        RedirectVelocity();

        playerMovement.enabled = false;

        //sliding marked true
        sliding = true;
        initCamPos.y = playerVision.transform.localPosition.y;

        //Get player's velocty in x and z direction (left and right).
        float xDir = playerMovement.getDirectionalVelo().x;
        float zDir = playerMovement.getDirectionalVelo().y;
        
        //Maintain player's velocity in direction with "slide" movement boost
        if(xDir == 0 && zDir == 0)
        {
            dirSlide = false;
            movement = transform.forward * 1;
        } else
        {
            movement = transform.right * xDir + transform.forward * zDir;
            dirSlide = true;
        }
        

        yield return new WaitForSeconds(slideTime);

        playerMovement.enabled = true;
        Debug.Log("Movement restored");
        sliding = false;
        playerVision.transform.localPosition = new Vector3(playerVision.transform.localPosition.x, initCamPos.y, playerVision.transform.localPosition.z);

        playerMovement.Jump(true);
        if(dirSlide = true)
        {
            dirSlide = false;
        }
    }

    //Jackson Methods
    public bool IsSliding()
    {
        return sliding;
    }

    private IEnumerator SlideInput()
    {
        tryingToSlide = true;
        if (canBuffer)
            yield return new WaitForSeconds(bufferTimer);
        else
            yield return new WaitForEndOfFrame();
        tryingToSlide = false;
    }

    private void RedirectVelocity()
    {
        Vector3 velocity = playerMovement.GetOutsideVelocity();
        float speed = Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z);
        Vector3 movement = transform.forward;
        Vector3 newVelocity = new Vector3(movement.x * speed, 0, movement.z * speed);
        playerMovement.SetOutsideVelocity(newVelocity, false);
    }

    public bool isTryingToSlide()
    {
        return tryingToSlide;
    }
}