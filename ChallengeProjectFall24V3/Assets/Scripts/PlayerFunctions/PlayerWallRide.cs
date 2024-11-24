using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.Windows;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
    public float tiltSpeed;
    public float tiltAmount;
    private float elapsedTime = 0;
    private Vector3 camStartRot;
    public Camera cam;
    private bool resetCameraTilt = false;
    private bool isTilting;
    private Vector3 curRot;
    public AudioClip wallRideSFX;
    private bool once2 = true;

    private SpeedLineManager speedLines;

    void Awake()
    {
        input = new MainInput();
        cooldownStart = wallJumpCooldown;
        camStartRot = cam.transform.rotation.eulerAngles;

        speedLines = transform.parent.GetChild(0).GetChild(3).GetComponent<SpeedLineManager>();
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
            once2 = true;

            //Get player's velocty in x and z direction (left and right).
            float xDir = playerMovement.getDirectionalVelo().x;
            float zDir = playerMovement.getDirectionalVelo().y;

            if (once)
            {
                transform.parent.GetComponent<AudioSource>().PlayOneShot(wallRideSFX);

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
            resetCameraTilt = false;
            TiltCamera();
            once2 = true;
        }
        else
        {
            playerMovement.enabled = true;
            once = true;
            resetCameraTilt = true;
        }

        //release jump button
        if(((onWall && input.Ground.Wallride.ReadValue<float>() <= 0) || !onWall) && wallRiding)
        {
            playerMovement.Jump(true);
            wallJumpCooldown = cooldownStart;
            wallRiding = false;
            once = true;
            resetCameraTilt = true;
            curRot = cam.transform.eulerAngles;
        }

        //??????????????????????
        if(input.Ground.Wallride.ReadValue<float>() <= 0)
        {
            playerMovement.enabled = true;
            resetCameraTilt = true;
        }

        //bug fix for sound
        if(input.Ground.Wallride.ReadValue<float>() > 0 && !onWall && once2)
        {
            transform.parent.GetComponent<AudioSource>().Stop();
            once2 = false;
        }

        //check if on wall
        onWall = Physics.CheckBox(transform.position, new Vector3(wallRiderSize,1,1), Quaternion.identity, wallLayer);

        //reset camera tilt if tilted
        if(resetCameraTilt)
        {
            ResetCameraTilt(curRot);
        }

        //speed lines
        if(!wallRiding || !onWall)
        {
            speedLines.setTrigger(2, false);
        }
    }

    private void LateUpdate()
    {
        if (wallRiding)
        {
            speedLines.setTrigger(2, true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(wallRiderSize, 1, 1));
    }

    private void TiltCamera()
    {
        if (!isTilting)
        {
            isTilting = true;
            elapsedTime = 0f; 
        }

        //check whether the wall is on our right or left
        bool onLeft = false;
        RaycastHit hit;
        //check left
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1f, wallLayer))
        {
            onLeft = true;
            //Debug.Log("wall on left for tilt");
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1f, wallLayer))
        {
            onLeft = false;
            //Debug.Log("wall on right for tilt");
        }
        float tiltChange = onLeft ? -tiltAmount : tiltAmount;

        if(elapsedTime < 1f)
        {
            elapsedTime = Mathf.Clamp01(elapsedTime + Time.deltaTime * tiltSpeed);
            Vector3 newRot = new Vector3(cam.transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, camStartRot.z + tiltChange);
            //Debug.Log(newRot);
            cam.transform.rotation = Quaternion.Lerp(Quaternion.Euler(camStartRot), Quaternion.Euler(newRot), elapsedTime);
        }

        
    }

    private void ResetCameraTilt(Vector3 tiltedRot)
    {
        if (isTilting)
        {
            
            elapsedTime = Mathf.Clamp01(elapsedTime - Time.deltaTime * tiltSpeed);
            cam.transform.rotation = Quaternion.Lerp(Quaternion.Euler(camStartRot), Quaternion.Euler(tiltedRot), elapsedTime);

            if (elapsedTime <= 0f)
            {
                isTilting = false;
            }
        }
    }
}
