using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    PlayerController owner;

    float timer = 0f;
    public float ownedRotationSpeed = 5f;
    public float ownedRotationRadius = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        if(owner != null)
        {
            Vector3 offset = new Vector3 (Mathf.Sin(ownedRotationSpeed * timer), 0f, Mathf.Cos(ownedRotationSpeed * timer));
            transform.position = owner.transform.position + ownedRotationRadius * offset;
            timer += Time.deltaTime;
        }
    }

    public void SetOwner(PlayerController _owner)
    {
        owner = _owner;

        timer = 0f;

        GetComponentInChildren<Collider>().enabled = false;
    }
}