using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime, Space.Self);
    }
}
