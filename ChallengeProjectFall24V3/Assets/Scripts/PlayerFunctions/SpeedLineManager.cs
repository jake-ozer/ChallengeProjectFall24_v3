using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLineManager : MonoBehaviour
{
    [SerializeField] int numOfSources;
    [SerializeField] private bool[] trigger;

    private ParticleSystem.MainModule main;
    // Start is called before the first frame update
    void Start()
    {
        trigger = new bool[numOfSources];
        for(int i = 0; i < numOfSources; i++)
        {
            trigger[i] = false;
        }

        main = GetComponent<ParticleSystem>().main;
    }

    // Update is called once per frame
    void Update()
    {
        main.startLifetime = shouldDisplay();
    }

    public void setTrigger(int i, bool trigger)
    {
        this.trigger[i] = trigger;
    }

    private int shouldDisplay()
    {
        for(int i = 0; i < numOfSources; i++)
        {
            if (trigger[i] == true) return 1;
        }
        return 0;
    }
}
