using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour, ITakeHit, ITargetEffect
{
    [SerializeField]
    private GameObject[] effectObjects;
    [SerializeField]
    bool canBeHit;
    [SerializeField]
    [Tooltip("If value is negative, event will be permanent")]
    int eventTimer;
    [SerializeField]
    [Tooltip("If this value is negative it will not reactivate")]
    float targetCooldown;
    [SerializeField]
    float effectStartDelay;

    [Header("Do Not Change")]
    [SerializeField]
    private Material activeMat;
    [SerializeField]
    private Material deactiveMat;

    private int remainingTime;

    private GameObject fuse;

    [SerializeField]
    [Tooltip("Only disable this when the attached effect object is a moving platform and you only want the target to be active when the player is standing on the platform")]
    private bool enabledFromStanding;


    // Start is called before the first frame update
    void Start()
    {
        fuse = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeHit && enabledFromStanding)
            fuse.GetComponent<MeshRenderer>().material = activeMat;
        else
            fuse.GetComponent<MeshRenderer>().material = deactiveMat;

    }

    public void Hit(int dmg)
    {
        if (canBeHit && enabledFromStanding)
        {
            Debug.Log("Shot Target!");
            for (int i = 0; i < effectObjects.Length; i++)
            {
                effectObjects[i].GetComponent<ITargetEffect>().Effect(eventTimer);
            }
            canBeHit = false;

            if (eventTimer > 0)
            {
                remainingTime = eventTimer;
                StartCoroutine(EventTimer());
            }
            else if(targetCooldown > 0)
            {
                StartCoroutine(Cooldown());
            }

        }

    }

    private IEnumerator EventTimer()
    {
        //displayTimer = true;
        yield return new WaitForSeconds(1);
        remainingTime--;
        if (remainingTime > 0)
        {
            StartCoroutine(EventTimer());
        }
        else if(targetCooldown > 0)
        {
            StartCoroutine(Cooldown());
        }

        
    }
    private IEnumerator Cooldown()
    {
        Debug.Log("Cooldown");
        yield return new WaitForSeconds(targetCooldown);
        canBeHit = true;
    }

    public void changeState()
    {
        canBeHit = !canBeHit;
    }

    public void changeState(bool state)
    {
        canBeHit = state;
    }

    public void Effect(int eventTimer)
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(effectStartDelay);
        gameObject.GetComponent<Target>().changeState(true);
    }

    public void updateStanding(bool standing)
    {
        enabledFromStanding = standing;
    }
}
