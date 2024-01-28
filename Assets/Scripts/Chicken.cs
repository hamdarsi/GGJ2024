using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    PlayerController owner;

    int ownedIndex;

    public float ownedRotationSpeed = 5f;
    public float ownedRotationRadius = 1f;

    public float oscilation = 0.5f;
    public float oscilationSpeed = 1f;

    void Start()
    {
    }

    void Update()
    {
        if(owner != null)
        {
            float initTheta = (ownedIndex * 360f / owner.ownedChickens.Count) * Mathf.Deg2Rad;
            float theta = ownedRotationSpeed * Time.time + initTheta;
            Vector3 offset = new Vector3 (Mathf.Sin(theta), 0f, Mathf.Cos(theta));
            transform.position = owner.transform.position + ownedRotationRadius * offset;
        }
        else
        {
            transform.position = new Vector3(transform.position.x
                , oscilation * (Mathf.Sin(Time.time * oscilationSpeed) + 1f) / 2f
                , transform.position.z);
        }
    }

    public void SetOwner(PlayerController _owner)
    {
        owner = _owner;
        ownedIndex = owner.ownedChickens.Count;
        GetComponentInChildren<Collider>().enabled = false;
        transform.localScale = 0.5f * Vector3.one;
    }

    public bool IsOwned()
    {
        return owner != null;
    }
}