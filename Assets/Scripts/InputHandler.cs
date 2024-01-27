using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerController carController;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carController == null) return;


        //carController.ApplyControls();
    }
}
