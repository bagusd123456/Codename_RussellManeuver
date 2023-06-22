using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected Coin");
            SoundManager.OnPlaySFXEvent(AudioLibrary.Instance.confirmSound);
        }
    }
}
