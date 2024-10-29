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
    private Vector3 moveDirection;
    [SerializeField]
    private float effectStartDelay;
    [SerializeField]
    private float resetSpeed;

    Vector3 moveDestination;
    bool moving;
    bool returning;

    private Vector3 spawnPosition;

    private float effectTimer;

    [SerializeField]
    private bool movePlayer;
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = null;
        movePlayer = false;
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
            if (movePlayer)
            {
                //player.transform.position = Vector3.MoveTowards(transform.position, moveDestination, moveSpeed * Time.deltaTime);
                player.GetComponent<PlayerMovement>().SetOutsideVelocity(moveDirection * moveSpeed, true);
            }
            transform.position = Vector3.MoveTowards(transform.position, moveDestination, moveSpeed * Time.deltaTime);

            if (transform.position == moveDestination)
            {
                moving = false;
                moveDestination = transform.position + moveDistance;
                if (movePlayer)
                    player.GetComponent<PlayerMovement>().SetOutsideVelocity(Vector3.zero, false); ;
            }
        }
        else if (returning)
        {
            if (movePlayer)
                player.GetComponent<PlayerMovement>().SetOutsideVelocity(-moveDirection * resetSpeed, true);
            transform.position = Vector3.MoveTowards(transform.position, spawnPosition, resetSpeed * Time.deltaTime);
            if (transform.position == spawnPosition)
            {
                returning = false;
                moveDestination = transform.position + moveDistance;
                if (movePlayer)
                    player.GetComponent<PlayerMovement>().SetOutsideVelocity(Vector3.zero, false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            player = other.gameObject;
            movePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            movePlayer = false;
        }
    }
}
