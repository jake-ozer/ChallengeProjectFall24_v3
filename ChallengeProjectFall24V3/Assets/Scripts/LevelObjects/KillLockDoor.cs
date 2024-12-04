using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class KillLockDoor : MonoBehaviour
{
    public List<Enemy> killList;
    private Animator anim;
    private bool first;
    public Slider bar;
    private int startingCount;

    void Start()
    {
        first = true;
        anim = GetComponent<Animator>();
        startingCount = killList.Count;
        bar.maxValue = startingCount;
        bar.value = 0;
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

        //update bar
        bar.value = startingCount - killList.Count;
    }

    private void OpenDoor()
    {
        bar.gameObject.transform.parent.gameObject.SetActive(false);
        anim.SetBool("Open", true);
    }
}
