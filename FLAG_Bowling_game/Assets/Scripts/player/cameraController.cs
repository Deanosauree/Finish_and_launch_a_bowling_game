using UnityEngine;
using UnityEngine.InputSystem;

public class cameraController : MonoBehaviour
{
    public enum enCameraPanDirections { none, up, down, left, right };

    Camera thisCamera;

    private Vector3 screenLocation = new Vector3(-35, 0, 0);
    private Vector3 ballsLocationne = new Vector3(0, -90, 0);
    private Vector3 laneLocation =new Vector3(0,0,0);

    public enum directions { lane, balls, screen };
    directions currentDirection = directions.lane;


    void Start()
    {
        thisCamera = GetComponentInChildren<Camera>();
        
    }

    public void PanCamera(enCameraPanDirections direction)
    {
        switch (direction) 
        { 
            case enCameraPanDirections.none:
                break;
            case enCameraPanDirections.up:
                thisCamera.transform.rotation = Quaternion.Euler(screenLocation);
                currentDirection = directions.screen;
                break;
            case enCameraPanDirections.down:
                thisCamera.transform.rotation = Quaternion.Euler(laneLocation);
                currentDirection = directions.lane;
                break;
            case enCameraPanDirections.left:
                thisCamera.transform.rotation = Quaternion.Euler(ballsLocationne);
                currentDirection = directions.balls;
                break;
            case enCameraPanDirections.right:
                thisCamera.transform.rotation = Quaternion.Euler(laneLocation);
                currentDirection = directions.lane;
                break;
        }
    }

    public directions getDirection()
    {
        return currentDirection;
    }
}
