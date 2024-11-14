using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Necessary Game Objects").transform.GetChild(0).transform.position = transform.position;
    }

}
