using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static cameraController;

public class inputManager : MonoBehaviour
{


    PlayerInput playerInputs;
    InputAction interactAction;
    InputAction panAction;
    InputAction lookAction;
    cameraController cameraControl;


    void Start()
    {
        playerInputs = GetComponent<PlayerInput>();
        cameraControl = GetComponent<cameraController>();

        lookAction = playerInputs.actions["look"];
        interactAction = playerInputs.actions["interact"];
        panAction = playerInputs.actions["pan"];


        lookAction.performed += GetInputs;
        interactAction.performed += GetInputs;
        panAction.performed += GetInputs;

    }

    private void GetInputs(InputAction.CallbackContext context)
    {
        Debug.Log(context.action);
        if (context.action == lookAction)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            Debug.Log(direction);
            enCameraPanDirections thisDirection = enCameraPanDirections.none;
            if (direction.x == 1)
            {
                thisDirection = enCameraPanDirections.right;
            }
            else if (direction.x == -1)
            {
                thisDirection = enCameraPanDirections.left;
            }
            else if (direction.y == 1)
            {
                thisDirection = enCameraPanDirections.up;
            }
            else if (direction.y == -1)
            {
                thisDirection = enCameraPanDirections.down;
            }
            else
            {
                
            }


            if (thisDirection != enCameraPanDirections.none)
            {
                cameraControl.PanCamera(thisDirection);
            }
        }

        if (context.action == interactAction) 
        {
            interact();
        }
        if (context.action == panAction) { }
    }

    private void interact()
    {

    }
}

