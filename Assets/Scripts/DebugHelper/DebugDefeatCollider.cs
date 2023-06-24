using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDefeatCollider : MonoBehaviour
{
    [SerializeField]
    private BoxCollider m_HitBox;
    // Start is called before the first frame update
    void OnValidate()
    {
        if(m_HitBox == null)
            m_HitBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("GameOver");
            GameManager.OnStateLoseCondition?.Invoke();
        }
    }
    private void OnDrawGizmos()
    {
        if (m_HitBox == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, m_HitBox.size);
    }
}
