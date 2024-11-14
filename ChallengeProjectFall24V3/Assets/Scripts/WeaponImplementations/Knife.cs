using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private float timer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        timer = 4;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (input.GetKeyDown(KeyCode.v))
        {
            Swing();
        }
    }

    private void Swing()
    {

    }
}
