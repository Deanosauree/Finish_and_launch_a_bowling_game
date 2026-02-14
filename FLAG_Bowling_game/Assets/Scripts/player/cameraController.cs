using UnityEngine;
using UnityEngine.InputSystem;

public class cameraController : MonoBehaviour
{
    public enum enCameraPanDirections { none, up, down, left, right };

    Camera thisCamera;

    private Vector3 screenLocation = new Vector3(-35, 0, 0);
    private Vector3 ballsLocationne = new Vector3(0, -90, 0);
    private Vector3 laneLocation =new Vector3(0,0,0);
    


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
                break;
            case enCameraPanDirections.down:
                thisCamera.transform.rotation = Quaternion.Euler(laneLocation);
                break;
            case enCameraPanDirections.left:
                thisCamera.transform.rotation = Quaternion.Euler(ballsLocationne);
                break;
            case enCameraPanDirections.right:
                thisCamera.transform.rotation = Quaternion.Euler(laneLocation);
                break;
        }
    }
}
