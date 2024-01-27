using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float gas;
    float steering;

    void Start()
    {        
    }


    
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //gas , steering -> Force + torque
    }

    private void Reset()
    {
    }

    public void ApplyControls(float _gas, float _steering)
    {
        gas = _gas;
        steering = _steering;
    }
}