using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearPlatform : MonoBehaviour
{
    [Header("Disappear Parameter")]

    [Tooltip("Kondisi awal platform")]
    public bool active = false;
    [Tooltip("Waktu yang dibutuhkan sebelum platform menghilang")]
    public float disappearTime = 1f;
    [Tooltip("Waktu awal platform sebelum menghilang")]
    public float startTime;
    float currentTime;

    [Header("Indicator Parameter")]

    [Tooltip("Waktu interval munculnya indicator")]
    public float indicatorInterval;
    float lastDisappearTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        DisappearIndicator();

        if (currentTime <= 0)
            TogglePlatform(active);
    }

    public void DisappearIndicator()
    {
        // Disappear Indicator
        if (!active)
        {
            if(currentTime < disappearTime - 1f)
            {
                if(lastDisappearTime < 0f)
                {
                    var meshRenderer = gameObject.GetComponent<MeshRenderer>();
                    meshRenderer.enabled = !meshRenderer.enabled;
                    lastDisappearTime = indicatorInterval;
                    //InvokeRepeating("Blink", 0, indicatorInterval);
                    //lastDisappearTime = indicatorInterval;
                }
                else
                {
                    lastDisappearTime -= Time.deltaTime;
                }
            }
        }
    }

    IEnumerator Blink()
    {
        var meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        meshRenderer.enabled = true;
    }

    public void TogglePlatform(bool state)
    {
        active = !active;
        currentTime = disappearTime;
        gameObject.GetComponent<Collider>().enabled = state;
        gameObject.GetComponent<MeshRenderer>().enabled = state;
    }
}
