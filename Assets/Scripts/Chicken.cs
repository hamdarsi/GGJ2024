using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    PlayerController owner;

    int ownedIndex;

    public float ownedRotationSpeed = 5f;
    public float ownedRotationRadius = 1f;

    void Start()
    {
    }

    void Update()
    {
        if(owner != null)
        {
            float initTheta = (ownedIndex * 360f / owner.nOwnedChickens) * Mathf.Deg2Rad;
            float theta = ownedRotationSpeed * Time.time + initTheta;
            Vector3 offset = new Vector3 (Mathf.Sin(theta), 0f, Mathf.Cos(theta));
            transform.position = owner.transform.position + ownedRotationRadius * offset;
        }
    }

    public void SetOwner(PlayerController _owner)
    {
        owner = _owner;

        ownedIndex = owner.nOwnedChickens;
        owner.nOwnedChickens++;

        GetComponentInChildren<Collider>().enabled = false;
    }

    public bool IsOwned()
    {
        return owner != null;
    }
}