using UnityEngine;

public class ShotgunPellet : MonoBehaviour
{
    [SerializeField] private float _lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy(gameObject);
    }
}