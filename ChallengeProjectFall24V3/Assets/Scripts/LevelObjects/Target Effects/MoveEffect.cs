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

    private bool movePlayer;

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
        //Moves platform towards destination
        if (moving && !returning)
        {
            //Moves player if they are standing on it
            if (movePlayer)
                player.GetComponent<PlayerMovement>().SetOutsideVelocity(moveDirection * moveSpeed, true);
            
            transform.position = Vector3.MoveTowards(transform.position, moveDestination, moveSpeed * Time.deltaTime);

            //Stops moving once it reaches it's destination
            if (transform.position == moveDestination)
            {
                moving = false;
                moveDestination = transform.position + moveDistance;
                //Moves player
                if (movePlayer)
                    player.GetComponent<PlayerMovement>().SetOutsideVelocity(Vector3.zero, false); ;
            }
        }

        //Moves platform back to original position
        else if (returning)
        {
            //Moves player if standing on it
            if (movePlayer)
                player.GetComponent<PlayerMovement>().SetOutsideVelocity(-moveDirection * resetSpeed, true);

            transform.position = Vector3.MoveTowards(transform.position, spawnPosition, resetSpeed * Time.deltaTime);

            //Stops once platform reaches original spot
            if (transform.position == spawnPosition)
            {
                returning = false;
                moveDestination = transform.position + moveDistance;

                //Stops player if they are standing on it
                if (movePlayer)
                    player.GetComponent<PlayerMovement>().SetOutsideVelocity(Vector3.zero, false);
            }
        }

    }

    //Called by Target object when shot
    public void Effect(int effectTimer)
    {
        this.effectTimer = effectTimer;
        StartCoroutine(EffectStartDelay());
    }
    
    //Waits to start effect
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

    //If player is standing on platform, makes it so player moves as well
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            player = other.gameObject;
            movePlayer = true;
        }
    }

    //Once player leaves platform stops moving them
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            movePlayer = false;
        }
    }
}
