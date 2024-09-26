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
    private Vector3 initCamPos;
    private Vector3 movement;

    // Start is called before the first frame update
    void Awake()
    {
        input = new MainInput();
        initCamPos = playerVision.transform.position;
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
        if (input.Ground.Slide.triggered && !sliding && playerMovement.onGround()) //make sure player can't slide in air or when already sliding
        {
            Debug.Log("You pressed slide");
            StartCoroutine("Slide");

        }


        if(sliding)
        {
            playerVision.transform.Translate(new Vector3(0, -cameraMoveSpeed, 0));
            controller.Move(movement * (playerMovement.speed * 1.1f) * Time.deltaTime * slideSpeed);
        }
    }
    private IEnumerator Slide()
    {
        playerMovement.enabled = false;

        //sliding marked true
        sliding = true;
        initCamPos.y = playerVision.transform.position.y;

        //Get player's velocty in x and z direction (left and right).
        float xDir = playerMovement.getDirectionalVelo().x;
        float zDir = playerMovement.getDirectionalVelo().y;

        //Maintain player's velocity in direction with "slide" movement boost
        movement = transform.right * xDir + transform.forward * zDir;

        yield return new WaitForSeconds(slideTime);

        playerMovement.enabled = true;
        Debug.Log("Movement restored");
        sliding = false;
        playerVision.transform.position = new Vector3(playerVision.transform.position.x, initCamPos.y, playerVision.transform.position.z);
        playerMovement.Jump();
    }
}