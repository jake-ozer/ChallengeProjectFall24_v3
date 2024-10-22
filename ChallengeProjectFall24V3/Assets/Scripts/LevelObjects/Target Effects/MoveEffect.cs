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

    Vector3 moveDestination;
    bool moving;
    

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        moveDestination = transform.position + moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDestination, moveSpeed * Time.deltaTime);

            if (transform.position == moveDestination)
            {
                moving = false;
                moveDestination = transform.position + moveDistance;
            }
        }

    }

    public void Effect()
    {
        StartCoroutine(EffectStartDelay());
    }
    
    private IEnumerator EffectStartDelay()
    {
        yield return new WaitForSeconds(effectStartDelay);
        moving = true;
    }
}
