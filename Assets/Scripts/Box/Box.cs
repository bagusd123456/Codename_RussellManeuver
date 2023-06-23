using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public enum boxState { Idle, Triggered, Blow}
    public boxState _boxState = boxState.Idle;
    public float radius = 5f;
    public float blowTime;
    public float blowCountDown;
    
    [Space]
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        blowCountDown = blowTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_boxState == boxState.Idle) return;

        blowCountDown -= Time.deltaTime;

        DisplayCountdown();

        if (_boxState == boxState.Triggered)
        {
            if (blowCountDown <= 0f)
            {
                Blow();
            }
        }
    }

    public void DisplayCountdown()
    {
        int i = Mathf.FloorToInt(blowCountDown);
        if (i < sprites.Length && i >= 0)
            spriteRenderer.sprite = sprites[i];
    }

    public void TriggerBehaviour()
    {
        if(_boxState == boxState.Idle)
            _boxState = boxState.Triggered;
    }

    public void Blow()
    {
        _boxState = boxState.Blow;

        Debug.Log("Box " + transform.name + " Blow");
        Collider[] collider = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in collider)
        {
            /*Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(1000f, transform.position, radius);
            }*/

            Box boxTarget;
            if (col.TryGetComponent(out boxTarget))
            {
                if(boxTarget._boxState != boxState.Blow)
                    boxTarget.Blow();
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            TriggerBehaviour();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
