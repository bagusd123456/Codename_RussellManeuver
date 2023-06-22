using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    public AudioClip clickSound, confirmSound, cancelSound;

    public static AudioLibrary Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
