using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuToEnable;

    public void EnableMenu()
    {
        menuToEnable.SetActive(true);
    }

    public void DisableMenu()
    {
        menuToEnable.SetActive(false);
    }
}
