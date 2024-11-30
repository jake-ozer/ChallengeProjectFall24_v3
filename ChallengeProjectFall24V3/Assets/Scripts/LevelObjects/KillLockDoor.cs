using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KillLockDoor : MonoBehaviour
{
    public List<Enemy> killList;
    private Animator anim;
    private bool first;

    void Start()
    {
        first = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //clear out null refs from list
        killList = killList.Where(i => i.alreadyDead == false).ToList();
       

        if(killList.Count == 0 && first == true)
        {
            first = false;
            OpenDoor();
        }

    }

    private void OpenDoor()
    {
        anim.SetBool("Open", true);
    }
}
