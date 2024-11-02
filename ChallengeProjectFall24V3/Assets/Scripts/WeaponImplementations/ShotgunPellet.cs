using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ShotgunPellet : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int damage;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ITakeHit>() != null)
        {
            //Debug.Log("You hit " + hit.collider.gameObject.name);
            other.gameObject.GetComponent<ITakeHit>().Hit(damage);
            Destroy(gameObject);
        }
    }
}