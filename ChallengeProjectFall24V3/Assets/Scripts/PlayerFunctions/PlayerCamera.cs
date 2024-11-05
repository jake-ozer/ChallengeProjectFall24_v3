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

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        Vector2 look = input.Ground.Look.ReadValue<Vector2>();
        float lookX = look.x * Time.deltaTime * xSens;
        float lookY = look.y * Time.deltaTime * ySens;
        //Debug.Log(Input.GetAxisRaw("Mouse X") + " " + Input.GetAxisRaw("Mouse Y"));
        //Debug.Log(look);


        xRot -= lookY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRot, yRot, transform.rotation.eulerAngles.z);
        transform.parent.Rotate(Vector3.up * lookX);
    }
}
