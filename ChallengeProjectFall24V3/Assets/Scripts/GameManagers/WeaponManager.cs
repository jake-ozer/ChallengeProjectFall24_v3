using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject curWeapon;
    public List<GameObject> weapons;
    private int num;
    private int num2;

    private void Start()
    {
        weapons = new List<GameObject>();
    }

    public IWeapon getCurWeapon()
    {
        return curWeapon.GetComponent<IWeapon>();
    }

    //hello hello
}
