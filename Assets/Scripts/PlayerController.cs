using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    int score = 0;

    public List<Chicken> ownedChickens = new List<Chicken>();

    private Vector2 steering;
    private Vector2 joyStickSteeringInput;
    private Vector2 keyboardSteeringInput;
    private bool frozen = true;
    
    private readonly List<KeyCode> keyboardKeys = new();

    public int playerNumber;
    public float forceMagnitude = 1f;
    public float rotationSpeed = 10f;
    public Material material;
    public GameObject materialSurface;

    public AudioSource audioIdle;
    public AudioSource audioRunning;

    float lastCollisionTime = 0f;
    public float collisionCooldownTime = 1f;
    public float stunMaxTime = 1;

    bool stunned = false;
    float stunTimer;
    public float stunRotationSpeed = 3f;

    bool attacking = false;
    public float attackForce = 100;
    public float attackLength = 0.5f;
    public float attackCooldown = 1f;
    float attackTimer = 0f;

    public TextMeshProUGUI scoreText;

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
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        if (frozen)
            return;
        
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

        if (stunned)
        {
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * stunRotationSpeed);
            stunTimer -= Time.fixedDeltaTime;
            if(stunTimer < 0)
            {
                stunned = false;
            }
        }
        else
        {
            steering = keyboardSteeringInput.sqrMagnitude > 0 ? keyboardSteeringInput : joyStickSteeringInput;

            var rb = GetComponent<Rigidbody>();
            var force = new Vector3(steering.x, 0f, steering.y);

            rb.AddForce(forceMagnitude * force);

            if (rb.velocity.sqrMagnitude > 0.001f)
                transform.forward = Vector3.Lerp(transform.forward, rb.velocity.normalized, rotationSpeed * Time.fixedDeltaTime);
        }

        if (attacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
            {
                attacking = false;
            }
        }
    }

    public void OnPlayerDirectionInput(InputAction.CallbackContext context)
    {
        joyStickSteeringInput = context.ReadValue<Vector2>();
    }

    public void OnPlayerActionButtonPress(InputAction.CallbackContext context)
    {
        if (attacking || attackTimer > 0f) return;

        var rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * attackForce, ForceMode.Impulse);

        attacking = true;
        attackTimer = attackCooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        var c = other.gameObject.GetComponentInParent<Chicken>();
        if (c != null)
        {
            ownedChickens.Add(c);
            c.SetOwner(this);
            score += 10;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"
            && (Time.time - lastCollisionTime) > collisionCooldownTime)
        {
            lastCollisionTime = Time.time;

            PlayerController op = collision.gameObject.GetComponent<PlayerController>();

            if (!attacking)
            {
                if (ownedChickens.Count > 0)
                {
                    // remove one chicken
                    Chicken c = ownedChickens[ownedChickens.Count - 1];
                    GameObject.Destroy(c.gameObject);
                    ownedChickens.RemoveAt(ownedChickens.Count - 1);
                }
                else
                {
                    Stun();
                }
            }

            if (op.attacking)
            {
                Stun();
            }

            if(attacking)
            {
                score += 50;
            }
        }
    }

    public void Unfreeze()
    {
        frozen = false;
        audioRunning.Play();
    }

    public void Freeze()
    {
        frozen = true;
        audioIdle.Play();
    }
    void Stun()
    {
        if (stunned) return;

        stunned = true;
        stunTimer = stunMaxTime;
    }
}