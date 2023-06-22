using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    public Camera _targetCamera;
    void Awake()
    {
        if(_targetCamera == null)
        {
            _targetCamera = Camera.main;
        }
    }
    void LateUpdate()
    {
        transform.LookAt(transform.position + _targetCamera.transform.forward);
    }
}
