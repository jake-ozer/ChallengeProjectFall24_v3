using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public GameObject curWeapon;
    public GameObject pistolObj;
    public GameObject shotgunObj;
    public GameObject crosshairObj;

    private MainInput input;
    private int curWeaponIndex = 0;

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

    public IWeapon getCurWeapon()
    {
        return curWeapon.GetComponent<IWeapon>();
    }

    private void Update()
    {
        if (input.Ground.SwitchWeapon.triggered)
        {
            StartCoroutine("ToggleWeapon");
        }

        crosshairObj.GetComponent<Image>().sprite = curWeapon.GetComponent<IWeapon>().crosshair;
    }

    //swap between either pistol or shotgun
    private IEnumerator ToggleWeapon()
    {
        curWeapon.GetComponent<IWeapon>().ResetAnimState();

        //wait for a second so that anim state can reset
        yield return new WaitForSeconds(0.01f);

        //enable/disable correct go
        //enable shotgun
        if(pistolObj.activeSelf)
        {
            pistolObj.SetActive(false);
            shotgunObj.SetActive(true);
            curWeapon = shotgunObj;
            shotgunObj.GetComponent<Shotgun>().ResetShootTimer();
        }
        else //enable pistol
        {
            pistolObj.SetActive(true);
            shotgunObj.SetActive(false);
            curWeapon = pistolObj;
        }
        curWeapon.GetComponent<IWeapon>().ReEnableGFX();
    }

}
