using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public List<Chicken> ownedChickens = new List<Chicken>();

    private Vector2 steering;
    private Vector2 joyStickSteeringInput;
    private Vector2 keyboardSteeringInput;
    
    private readonly List<KeyCode> keyboardKeys = new();

    public int playerNumber;
    public float forceMagnitude = 1f;
    public float rotationSpeed = 10f;
    public Material material;
    public GameObject materialSurface;
    
    void Start()
    {
        if (playerNumber == 1)
        {
            keyboardKeys.Add(KeyCode.W);
            keyboardKeys.Add(KeyCode.S);
            keyboardKeys.Add(KeyCode.A);
            keyboardKeys.Add(KeyCode.D);
            keyboardKeys.Add(KeyCode.Space);
        }
        else if (playerNumber == 2)
        {
            keyboardKeys.Add(KeyCode.UpArrow);
            keyboardKeys.Add(KeyCode.DownArrow);
            keyboardKeys.Add(KeyCode.LeftArrow);
            keyboardKeys.Add(KeyCode.RightArrow);
            keyboardKeys.Add(KeyCode.Return);
        }

        materialSurface.GetComponent<MeshRenderer>().material = material;
    }

    void FixedUpdate()
    {
        keyboardSteeringInput.x = 0;
        keyboardSteeringInput.y = 0;
        if (keyboardKeys.Count > 0)
        {
            keyboardSteeringInput.y += Input.GetKey(keyboardKeys[0]) ? 1 : 0;
            keyboardSteeringInput.y += Input.GetKey(keyboardKeys[1]) ? -1 : 0;
            keyboardSteeringInput.x += Input.GetKey(keyboardKeys[2]) ? -1 : 0;
            keyboardSteeringInput.x += Input.GetKey(keyboardKeys[3]) ? 1 : 0;

            if (Input.GetKeyDown(keyboardKeys[4]))
                OnPlayerActionButtonPress(default);
        }

        steering = keyboardSteeringInput.sqrMagnitude > 0 ? keyboardSteeringInput : joyStickSteeringInput;
        
        var rb = GetComponent<Rigidbody>();
        var force = new Vector3(steering.x, 0f, steering.y);

        rb.AddForce(forceMagnitude * force);

        if (rb.velocity.sqrMagnitude > 0.001f)
          transform.forward = Vector3.Lerp(transform.forward, rb.velocity.normalized, rotationSpeed * Time.fixedDeltaTime);
    }

    public void OnPlayerDirectionInput(InputAction.CallbackContext context)
    {
        joyStickSteeringInput = context.ReadValue<Vector2>();
    }

    public void OnPlayerActionButtonPress(InputAction.CallbackContext context)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var c = other.gameObject.GetComponentInParent<Chicken>();
        if (c != null)
        {
            ownedChickens.Add(c);
            c.SetOwner(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log(Time.time);
            if (ownedChickens.Count > 0)
            {
                Chicken c = ownedChickens[ownedChickens.Count - 1];
                GameObject.Destroy(c.gameObject);
                ownedChickens.RemoveAt(ownedChickens.Count - 1);
            }
        }
    }
}