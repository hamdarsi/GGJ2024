using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 steering;

    void Start()
    {        
    }

    public float forceMagnitude = 1f;
    
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //gas , steering -> Force + torque

        steering.x = Input.GetAxis("Horizontal");
        steering.y = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(steering.x, 0f, steering.y);

        rb.AddForce(forceMagnitude * force);

        transform.forward = rb.velocity.normalized;
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