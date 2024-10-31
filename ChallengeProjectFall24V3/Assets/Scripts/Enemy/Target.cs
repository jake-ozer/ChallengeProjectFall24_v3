using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, ITakeHit, ITargetEffect
{
    [SerializeField]
    private GameObject[] effectObjects;
    [SerializeField]
    bool canBeHit;
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeHit)
            gameObject.GetComponent<MeshRenderer>().material = activeMat;
        else
            gameObject.GetComponent<MeshRenderer>().material = deactiveMat;
    }

    public void Hit(int dmg)
    {
        if (canBeHit)
        {
            Debug.Log("Shot Target!");
            for (int i = 0; i < effectObjects.Length; i++)
            {
                effectObjects[i].GetComponent<ITargetEffect>().Effect();
            }
            canBeHit = false;
            if(targetCooldown > 0)
            {
                StartCoroutine(Cooldown());
            }
        }

    }

    private IEnumerator Cooldown()
    {
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

    public void Effect()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(effectStartDelay);
        gameObject.GetComponent<Target>().changeState(true);
    }
}
