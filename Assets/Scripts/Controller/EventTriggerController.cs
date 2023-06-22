using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerController : MonoBehaviour
{
    public bool canTrigger = false;

    bool eventStarted = false;

    public static event Action<bool> OnTriggered;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            OnTriggered?.Invoke(true);

            canTrigger = true;
            if (Input.GetKeyDown(KeyCode.K) && !eventStarted)
            {
                eventStarted = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            OnTriggered?.Invoke(false);

            canTrigger = false;
            eventStarted = false;
        }
    }
}
