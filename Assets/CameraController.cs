using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Camera theCamera;

    public enum CameraMode {FLAT, THREE_D, ROTATE_LEFT, ROTATE_RIGHT, ANGLE, ANGLE_ROTATE};
    private CameraMode currentCameraMode = CameraMode.FLAT;

    private Vector3 flatCameraPosition = new Vector3(0, 4, -12);
    private Vector3 flatCameraRotation = new Vector3(0, 0, 0);
    private Vector3 angleCameraPosition = new Vector3(0, 12, -12);
    private Vector3 angleCameraRotation = new Vector3(35, 0, 0);
    private Vector3 threeDCameraPosition = new Vector3(0, 6,-12);
    private Vector3 threeDCameraRotation = new Vector3(10, 0, 0);

    private Coroutine currentCoroutine;

    private float rotateSpeed = 25;

    void Awake()
    {
        theCamera = GetComponent<Camera>();
        currentCoroutine = StartCoroutine(MoveCamera(flatCameraPosition, flatCameraRotation, CameraMode.FLAT));
    }

    public void SetCameraModeFlat()
    {
        //currentCameraMode = CameraMode.FLAT;
        theCamera.orthographic = true;

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(flatCameraPosition, flatCameraRotation, CameraMode.FLAT));

        //theCamera.transform.position = flatCameraPosition;
        //theCamera.transform.rotation = Quaternion.Euler(flatCameraRotation);
    }

    public void SetCameraMode3D()
    {
        //currentCameraMode = CameraMode.THREE_D;
        theCamera.orthographic = false;

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(threeDCameraPosition, threeDCameraRotation, CameraMode.THREE_D));

        //theCamera.transform.position = threeDCameraPosition;
        //theCamera.transform.rotation = Quaternion.Euler(threeDCameraRotation);
    }

    public void SetCameraModeRotateLeft()
    {
        //currentCameraMode = CameraMode.ROTATE_LEFT;
        theCamera.orthographic = false;

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(threeDCameraPosition, threeDCameraRotation, CameraMode.ROTATE_LEFT));
        //theCamera.transform.position = threeDCameraPosition;
        //theCamera.transform.rotation = Quaternion.Euler(threeDCameraRotation);
        print("DONE");
    }

    public void SetCameraModeRotateRight()
    {
        //currentCameraMode = CameraMode.ROTATE_RIGHT;
        theCamera.orthographic = false;

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(threeDCameraPosition, threeDCameraRotation, CameraMode.ROTATE_RIGHT));

        //theCamera.transform.position = threeDCameraPosition;
        //theCamera.transform.rotation = Quaternion.Euler(threeDCameraRotation);
    }

    public void SetCameraModeAngle()
    {

        //currentCameraMode = CameraMode.ANGLE;
        theCamera.orthographic = false;

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(angleCameraPosition, angleCameraRotation, CameraMode.ANGLE));

        //theCamera.transform.position = angleCameraPosition;
        //theCamera.transform.rotation = Quaternion.Euler(angleCameraRotation);
    }

    public void SetCameraModeAngleRotate()
    {

        //currentCameraMode = CameraMode.ANGLE_ROTATE;
        theCamera.orthographic = false;
        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(angleCameraPosition, angleCameraRotation, CameraMode.ANGLE_ROTATE));

        //theCamera.transform.position = angleCameraPosition;
        //theCamera.transform.rotation = Quaternion.Euler(angleCameraRotation);
    }

    IEnumerator MoveCamera(Vector3 position, Vector3 rotation , CameraMode newCameraMode)
    {
        Vector3 startPosition = theCamera.transform.position;
        Quaternion startRotation = theCamera.transform.rotation;
        for(float i = 0f; i < 1; i+= Time.deltaTime / 0.5f)
        {
            theCamera.transform.position = Vector3.Lerp(startPosition, position, i);
            theCamera.transform.rotation = Quaternion.Slerp(startRotation, Quaternion.Euler(rotation), i);
            yield return null;
        }
        currentCameraMode = newCameraMode;

    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCameraModeFlat();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCameraMode3D();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetCameraModeRotateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetCameraModeRotateRight();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetCameraModeAngle();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetCameraModeAngleRotate();
        }
        
        if(Input.GetKey(KeyCode.Minus))
        {
            rotateSpeed-=Time.deltaTime*20;
            if(rotateSpeed < 1)
            {
                rotateSpeed = 1;
            }
        }
        else if (Input.GetKey(KeyCode.Equals)) //actually the plus key
        {
            rotateSpeed += Time.deltaTime*20;
        }

        if (currentCameraMode == CameraMode.ROTATE_LEFT || currentCameraMode == CameraMode.ANGLE_ROTATE)
        {
            transform.RotateAround(Vector3.zero, Vector2.up, Time.deltaTime * rotateSpeed);
        }
        else if (currentCameraMode == CameraMode.ROTATE_RIGHT)
        {
            transform.RotateAround(Vector3.zero, Vector2.up, -Time.deltaTime * rotateSpeed);
        }
    }
}
