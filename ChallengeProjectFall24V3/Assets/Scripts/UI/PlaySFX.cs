using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioClip clip;

    public void PlayEffect()
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
