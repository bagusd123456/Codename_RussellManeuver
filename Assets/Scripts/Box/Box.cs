using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float radius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Blow()
    {
        Debug.Log("Box " + transform.name + " Blow");
        Collider[] collider = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in collider)
        {
            /*Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(1000f, transform.position, radius);
            }*/

            if(col.GetComponent<Box>() != null)
            {
                Box box = col.GetComponent<Box>();
                box.Blow();
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            Destroy(gameObject);
            //Blow();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
