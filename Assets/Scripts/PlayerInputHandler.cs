using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private ControllerInputActions controllerInputActions;
    public int playerNumber;

    private List<KeyCode> keyboardKeys = new List<KeyCode>();

    private PlayerController playerController;
    private PlayerInput playerInput;

    void Start()
    {
       //controllerInputActions = GetComponent<ControllerInputActions>();
       playerController = GetComponent<PlayerController>();
       playerInput = GetComponent<PlayerInput>();

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
    }

    void Update()
    {
        /*playerInput.ac
        var actionPressed = controllerInputActions.Player.Action.IsPressed();
        var value = controllerInputActions.Player.Direction.ReadValue<Vector2>();

        var vector = new Vector3
        {
            z = value.x,
            x = -value.y
        };

        if (!keyboardKeys.IsEmpty())
        {
            vector.x += Input.GetKey(keyboardKeys[0]) ? -1 : 0;
            vector.x += Input.GetKey(keyboardKeys[1]) ? 1 : 0;
            vector.z += Input.GetKey(keyboardKeys[2]) ? -1 : 0;
            vector.z += Input.GetKey(keyboardKeys[3]) ? 1 : 0;

            if (Input.GetKeyDown(keyboardKeys[4]))
                actionPressed = true;
        }

        playerController.ApplyControls(vector);*/
    }
}