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
    public float rotationSpeed = 10f;
    
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        steering.x = Input.GetAxis("Horizontal");
        steering.y = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(steering.x, 0f, steering.y);

        rb.AddForce(forceMagnitude * force);

        if (rb.velocity.sqrMagnitude > 0.001f)
        {
            transform.forward = Vector3.Lerp(transform.forward
                , rb.velocity.normalized, rotationSpeed * Time.fixedDeltaTime);
        }
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