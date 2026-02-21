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
    ballManager ballManager;

    private bool isShuffle = false;


    void Start()
    {
        playerInputs = GetComponent<PlayerInput>();
        cameraControl = GetComponent<cameraController>();
        ballManager = GetComponent<ballManager>();

        lookAction = playerInputs.actions["look"];
        interactAction = playerInputs.actions["interact"];
        panAction = playerInputs.actions["pan"];


        lookAction.performed += GetInputs;

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
    }

    public void OnPan(InputValue value)
    {
        float pressvalue = value.Get<float>();

        if (!isShuffle)
        {
            cameraControl.throwPan(pressvalue);
        }
        else 
        {
            cameraControl.throwShuffle(pressvalue);
        }
        
    }

    public void OnPanState(InputValue value)
    {
        float pressvalue = value.Get<float>();
        switch (pressvalue) 
        { 
            case -1:
                isShuffle = false; break;
            case 1:
                isShuffle = true; break;
            default:
                break;
        }
    }

    public void OnInteract()
    {
        directions direction = cameraControl.getDirection();
        switch (direction) 
        {
            case (directions.lane):
                ballManager.throwBall();
                break;
            default: break;
        
        }
    }
}

