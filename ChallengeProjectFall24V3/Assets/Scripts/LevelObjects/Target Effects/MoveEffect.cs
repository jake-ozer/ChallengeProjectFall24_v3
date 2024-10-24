using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEffect : MonoBehaviour, ITargetEffect
{

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector3 moveDistance;
    [SerializeField]
    private float effectStartDelay;
    [SerializeField]
    private float resetSpeed;

    Vector3 moveDestination;
    bool moving;
    bool returning;

    private Vector3 spawnPosition;

    private float effectTimer;
    

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        returning = false;
        moveDestination = transform.position + moveDistance;
        spawnPosition = transform.position;
        effectTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && !returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDestination, moveSpeed * Time.deltaTime);

            if (transform.position == moveDestination)
            {
                moving = false;
                moveDestination = transform.position + moveDistance;
            }
        }
        else if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPosition, resetSpeed * Time.deltaTime);
            if(transform.position == spawnPosition)
            {
                returning = false;
                moveDestination = transform.position + moveDistance;
            }
        }

    }

    public void Effect(int effectTimer)
    {
        this.effectTimer = effectTimer;
        StartCoroutine(EffectStartDelay());
    }
    
    private IEnumerator EffectStartDelay()
    {
        yield return new WaitForSeconds(effectStartDelay);
        moving = true;
        if(effectTimer >= 0)
        {
            yield return new WaitForSeconds(effectTimer);
            returning = true;
        }
    }
}
