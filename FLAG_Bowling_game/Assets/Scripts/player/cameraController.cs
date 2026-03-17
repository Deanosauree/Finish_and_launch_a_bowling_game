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
    [SerializeField] float lookSpeed;
    Camera thisCamera;

    private Vector3 screenLocation = new Vector3(-35, 0, 0);
    private Vector3 ballsLocationne = new Vector3(0, -90, 0);
    private Vector3 laneLocation =new Vector3(0,0,0);
    private Vector3 shopLocation = new Vector3(0, 90, 0);
    private float[] zooms = {30,40,60 };
    private Vector3 defaultLocation;
    private Vector3 cameraEulers = new Vector3(0, 0, 0);

    public bool bowling = false;
    private IEnumerator lookRoutine;

    private Vector2 currentShufflePan = new Vector2(0,0);
    public enum directions { lane, balls, screen, shop};
    directions currentDirection = directions.lane;


    void Start()
    {
        thisCamera = GetComponentInChildren<Camera>();
        defaultLocation = thisCamera.transform.position;
        showLine(false);
    }

    public void PanCamera(enCameraPanDirections direction)
    {
        switch (direction) 
        { 
            case enCameraPanDirections.none:
                break;
            case enCameraPanDirections.up:
                Vector3 rotationGoal;
                if (currentDirection == directions.lane)
                {
                    rotationGoal = new Vector3(screenLocation.x, thisCamera.transform.rotation.y, 0);
                }
                else
                {
                    rotationGoal = screenLocation;
                }
                goTo(rotationGoal, thisCamera.transform.position);
                thisCamera.fieldOfView = zooms[0];
                currentDirection = directions.screen;
                showLine(false);
                break;
            case enCameraPanDirections.down:
                goTo(laneLocation + new Vector3(0, currentShufflePan.y, 0), defaultLocation + new Vector3(currentShufflePan.x, 0, 0));
                thisCamera.fieldOfView = zooms[2];
                currentDirection = directions.lane;
                break;
            case enCameraPanDirections.left:
                if (currentDirection == directions.shop)
                {
                    goTo(laneLocation + new Vector3(0, currentShufflePan.y, 0), defaultLocation + new Vector3(currentShufflePan.x, 0, 0));
                    thisCamera.fieldOfView = zooms[2];
                    currentDirection = directions.lane;
                }
                else
                {
                    goTo(ballsLocationne, defaultLocation);
                    thisCamera.fieldOfView = zooms[1];
                    currentDirection = directions.balls;
                    showLine(false);
                }
                break;
            case enCameraPanDirections.right:
                if (currentDirection == directions.balls)
                {
                    goTo(laneLocation + new Vector3(0, currentShufflePan.y, 0), defaultLocation + new Vector3(currentShufflePan.x, 0, 0));
                    thisCamera.fieldOfView = zooms[2];
                    currentDirection = directions.lane;
                }
                else
                {
                    thisCamera.fieldOfView = zooms[1];
                    goTo(shopLocation, defaultLocation);
                    currentDirection = directions.shop;
                    showLine(false);
                }
                break;
        }
    }

    public void showLine(bool visible)
    {
        if (bowling)
        {
            pointingLine.SetActive(visible);
        }
        else 
        { 
            pointingLine.SetActive(false);
        }

    }

    public void throwPan(float direction)
    {
        if (currentDirection == directions.lane & bowling)
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
        if (currentDirection == directions.lane & bowling)
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

    private void goTo(Vector3 rotTarget, Vector3 posTarget)
    {
        if (lookRoutine != null) { StopCoroutine(lookRoutine); }
        lookRoutine = SmoothLook(rotTarget, posTarget);
        StartCoroutine(lookRoutine);
    }

    private IEnumerator SmoothLook(Vector3 rotTarget, Vector3 posTarget)
    {
        Vector3 startingPosition = thisCamera.transform.position;
        Vector3 startingRotation = cameraEulers;
        int lookTime = (int)(10 / lookSpeed);
        for (int i = 0; i < lookTime; i++) 
        {
            Vector3 currentPos = thisCamera.transform.position;
            thisCamera.transform.position = new Vector3(currentPos.x + ((posTarget.x - startingPosition.x )/ lookTime), currentPos.y, currentPos.z + ((posTarget.z - startingPosition.z) / lookTime));
            cameraEulers = new Vector3(cameraEulers.x+((rotTarget.x-startingRotation.x)/lookTime), cameraEulers.y + ((rotTarget.y - startingRotation.y) / lookTime), 0);
            thisCamera.transform.rotation = Quaternion.Euler(cameraEulers);

            yield return new WaitForFixedUpdate();
        }
        if (currentDirection == directions.lane) { showLine(true); }
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
