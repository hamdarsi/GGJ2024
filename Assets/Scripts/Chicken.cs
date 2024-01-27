using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    PlayerController owner;

    void Start()
    {
        
    }

    void Update()
    {
        if(owner != null)
        {
            transform.position = owner.transform.position + new Vector3(0f, 0f, 1f);
            // Debug.DrawLine(owner.transform.position, owner.transform.position + Vector3.up, Color.yellow);
        }
    }

    public void SetOwner(PlayerController _owner)
    {
        owner = _owner;
    }
}