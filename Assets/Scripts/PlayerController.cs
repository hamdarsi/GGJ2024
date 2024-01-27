using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 steering;

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

    public void ApplyControls(Vector2 _steering)
    {
        steering = _steering;
    }

    private void OnTriggerEnter(Collider other)
    {
        Chicken c = other.gameObject.GetComponentInParent<Chicken>();
        if(c != null)
        {
            c.SetOwner(this);
        }
    }
}