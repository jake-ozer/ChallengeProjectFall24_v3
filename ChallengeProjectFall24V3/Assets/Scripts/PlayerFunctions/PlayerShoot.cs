using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    private MainInput input;

    private void Awake()
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
        if (input.Ground.Shoot.triggered)
        {
            weaponManager.getCurWeapon().Shoot();
        }
    }
}
