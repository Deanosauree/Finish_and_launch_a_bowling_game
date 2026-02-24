using System.Collections;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public enum enCameraPanDirections { none, up, down, left, right };
    [SerializeField] GameObject pointingLine;
    [SerializeField] float panStep;
    [SerializeField] float shuffleStep;
    [SerializeField] float panCap;
    [SerializeField] float shuffleCap;
    Camera thisCamera;

    private Vector3 screenLocation = new Vector3(-35, 0, 0);
    private Vector3 ballsLocationne = new Vector3(0, -90, 0);
    private Vector3 laneLocation =new Vector3(0,0,0);
    private float[] zooms = {30,40,60 };
    private Vector3 defaultLocation;

    private Vector2 currentShufflePan = new Vector2(0,0);
    public enum directions { lane, balls, screen };
    directions currentDirection = directions.lane;


    void Start()
    {
        thisCamera = GetComponentInChildren<Camera>();
        defaultLocation = thisCamera.transform.position;
        
    }

    public void PanCamera(enCameraPanDirections direction)
    {
        switch (direction) 
        { 
            case enCameraPanDirections.none:
                break;
            case enCameraPanDirections.up:
                if (currentDirection == directions.lane)
                {
                    thisCamera.transform.rotation = Quaternion.Euler(new Vector3(screenLocation.x, thisCamera.transform.rotation.y, 0));
                }
                else
                {
                    thisCamera.transform.rotation = Quaternion.Euler(screenLocation);
                }
                thisCamera.fieldOfView = zooms[0];
                currentDirection = directions.screen;
                pointingLine.SetActive(false);
                break;
            case enCameraPanDirections.down:
                thisCamera.transform.rotation = Quaternion.Euler(laneLocation + new Vector3(0,currentShufflePan.y, 0));
                thisCamera.fieldOfView = zooms[2];
                thisCamera.transform.position = defaultLocation + new Vector3(currentShufflePan.x,0,0);
                currentDirection = directions.lane;
                pointingLine.SetActive(true);
                break;
            case enCameraPanDirections.left:
                thisCamera.transform.rotation = Quaternion.Euler(ballsLocationne);
                thisCamera.fieldOfView = zooms[1];
                currentDirection = directions.balls;
                thisCamera.transform.position = defaultLocation;
                pointingLine.SetActive(false);
                break;
            case enCameraPanDirections.right:
                thisCamera.transform.rotation = Quaternion.Euler(laneLocation + new Vector3(0, currentShufflePan.y, 0));
                thisCamera.fieldOfView = zooms[2];
                thisCamera.transform.position = defaultLocation + new Vector3(currentShufflePan.x, 0, 0);
                currentDirection = directions.lane;
                pointingLine.SetActive(true);
                break;
        }
    }

    public void throwPan(float direction)
    {
        if (currentDirection == directions.lane)
        {
            currentShufflePan.y += panStep * direction;
            currentShufflePan.y = Mathf.Clamp(currentShufflePan.y, -panCap, panCap);
            thisCamera.transform.rotation = Quaternion.Euler(new Vector3(0, currentShufflePan.y, 0));
            if (direction != 0)
            {
                StartCoroutine(ContinousPan(direction));
            }
        }
        if (direction == 0) { StopAllCoroutines(); }
    }

    public void throwShuffle(float direction)
    {
        if (currentDirection == directions.lane)
        {
            currentShufflePan.x += shuffleStep * direction;
            currentShufflePan.x = Mathf.Clamp(currentShufflePan.x, -shuffleCap, shuffleCap);
            thisCamera.transform.position = defaultLocation + new Vector3(currentShufflePan.x, 0, 0);
            if (direction != 0)
            {
                StartCoroutine(ContinousShuffle(direction));
            }
        }
        if (direction == 0) { StopAllCoroutines(); }
    }

    private IEnumerator ContinousPan(float direction)
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            currentShufflePan.y += panStep * direction;
            currentShufflePan.y = Mathf.Clamp(currentShufflePan.y, -panCap, panCap);
            thisCamera.transform.rotation = Quaternion.Euler(new Vector3(0, currentShufflePan.y, 0));
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator ContinousShuffle(float direction)
    {
        yield return new WaitForSeconds(0.5f);
        while (true) 
        {
            currentShufflePan.x += shuffleStep * direction;
            currentShufflePan.x = Mathf.Clamp(currentShufflePan.x, -shuffleCap, shuffleCap);
            thisCamera.transform.position = defaultLocation + new Vector3(currentShufflePan.x, 0, 0);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public directions getDirection()
    {
        return currentDirection;
    }
}
