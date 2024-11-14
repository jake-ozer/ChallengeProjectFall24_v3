using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleDoor : MonoBehaviour
{
    [Header("Duration")]
    [SerializeField]
    private float openDuration;
    [SerializeField]
    private float closeDuration;

    [Header("Speed")]
    [SerializeField]
    private float openSpeed;
    [SerializeField] 
    private float closeSpeed;

    [Header("Other")]
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private bool startOpened;

    private bool opening;
    private bool closing;

    private Vector3 closedPosition;
    private Vector3 openedPosition;

    // Start is called before the first frame update
    void Start()
    {
        opening = false;
        closing = false;
        closedPosition = transform.position;
        openedPosition = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
        if(startOpened)
        {
            transform.position = openedPosition;
        }
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
            transform.position = Vector3.MoveTowards(transform.position, openedPosition, openSpeed * Time.deltaTime);
        if (closing)
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, closeSpeed * Time.deltaTime);

    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        if (startOpened)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        opening = true;
        StartCoroutine(OpenDoorDelay());
    }

    private void CloseDoor()
    {
        closing = true;
        StartCoroutine(CloseDoorDelay());
    }

    private IEnumerator OpenDoorDelay()
    {
        yield return new WaitForSeconds(openDuration);
        opening = false;
        CloseDoor();
    }

    private IEnumerator CloseDoorDelay()
    {
        yield return new WaitForSeconds(closeDuration);
        closing = false;
        OpenDoor();
    }
}
