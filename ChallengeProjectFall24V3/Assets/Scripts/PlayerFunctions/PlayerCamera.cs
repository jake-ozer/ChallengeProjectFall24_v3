using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float xSens;
    public float ySens;

    private float xRot;
    private float yRot;
    private MainInput input;

    // Tilt variables
    private float tiltAmount = 0f; // Current tilt applied by wall riding
    private float tiltSpeed = 10f; // How quickly the tilt changes

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        input = new MainInput();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        Vector2 look = input.Ground.Look.ReadValue<Vector2>();

        // Smooth and clamp inputs
        float lookX = Mathf.Clamp(look.x * xSens, -10f, 10f);
        float lookY = Mathf.Clamp(look.y * ySens, -10f, 10f);

        // Update rotations
        xRot -= lookY;
        xRot = Mathf.Clamp(xRot, -90f, 90f); // Clamp pitch
        yRot += lookX;                      // Update yaw

        // Apply base rotations
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.parent.localRotation = Quaternion.Euler(0f, yRot, 0f);
    }

    public void SetTilt(float targetTilt, float speed)
    {
        // Smoothly update the tilt amount
        tiltAmount = Mathf.Lerp(tiltAmount, targetTilt, Time.deltaTime * speed);

        // Stop excessive tilting beyond target
        if (Mathf.Abs(tiltAmount - targetTilt) < 0.01f)
        {
            tiltAmount = targetTilt;
        }
    }

    private void LateUpdate()
    {
        // Apply tilt after base rotations
        Vector3 currentEulerAngles = transform.localRotation.eulerAngles;
        currentEulerAngles.z = tiltAmount;
        transform.localRotation = Quaternion.Euler(currentEulerAngles);
    }
}
