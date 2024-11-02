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

    [SerializeField]
    private TMP_Text messageText;

    private bool displayTimer;

    private int remainingTime;


    // Start is called before the first frame update
    void Start()
    {
        displayTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeHit)
            gameObject.GetComponent<MeshRenderer>().material = activeMat;
        else
            gameObject.GetComponent<MeshRenderer>().material = deactiveMat;

        if (displayTimer)
        {
            messageText.SetText(remainingTime.ToString());
        }
        else
        {
            messageText.SetText("");
        }
    }

    public void Hit(int dmg)
    {
        if (canBeHit)
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
        displayTimer = true;
        yield return new WaitForSeconds(1);
        remainingTime--;
        if (remainingTime > 0)
        {
            StartCoroutine(EventTimer());
        }
        else if(targetCooldown > 0)
        {
            displayTimer = false;
            StartCoroutine(Cooldown());
        }
        else
            displayTimer = false;
        
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
}
