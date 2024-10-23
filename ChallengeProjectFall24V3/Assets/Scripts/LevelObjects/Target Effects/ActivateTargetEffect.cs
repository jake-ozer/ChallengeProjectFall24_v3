using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTargetEffect : MonoBehaviour, ITargetEffect
{
    [SerializeField]
    private float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Effect(int eventTimer)
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        gameObject.GetComponent<Target>().changeState(true);
    }
}
