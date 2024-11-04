using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IWeapon
{
    Sprite crosshair
    {
        get; set;
    }

    public void Shoot();
    public void ResetAnimState();
    public void ReEnableGFX();
}
