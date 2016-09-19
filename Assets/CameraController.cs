using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;

    private Camera theCamera;

    public enum CameraMode {FLAT, THREE_D, ROTATE_LEFT, ROTATE_RIGHT, ANGLE, ANGLE_ROTATE};
    private CameraMode currentCameraMode = CameraMode.FLAT;
    private Vector3 initialCameraPosition;
    


    private float rotateSpeed = 20;

    void Awake()
    {
        theCamera = GetComponent<Camera>();
        initialCameraPosition = theCamera.transform.position;
    }

    public void SetCameraModeFlat()
    {
        if(currentCameraMode == CameraMode.FLAT)
        {
            return;
        }
        currentCameraMode = CameraMode.FLAT;
        theCamera.orthographic = true;
        theCamera.transform.position = initialCameraPosition;
        theCamera.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void SetCameraModeRotateLeft()
    {
        if (currentCameraMode == CameraMode.ROTATE_LEFT)
        {
            return;
        }
        currentCameraMode = CameraMode.ROTATE_LEFT;
        theCamera.orthographic = false;
        theCamera.transform.LookAt(target);
    }

    public void SetCameraModeRotateRight()
    {
        if (currentCameraMode == CameraMode.ROTATE_RIGHT)
        {
            return;
        }
        currentCameraMode = CameraMode.ROTATE_RIGHT;
        theCamera.orthographic = false;
        theCamera.transform.LookAt(target);
    }

    public void SetCameraMode3D()
    {

    }

    public void SetCameraModeAngle()
    {

    }

    public void SetCameraModeAngleRotate()
    {

    }
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCameraModeFlat();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCameraModeRotateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetCameraModeRotateRight();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {

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

        if (currentCameraMode == CameraMode.ROTATE_LEFT)
        {
            transform.RotateAround(Vector3.zero, Vector2.up, Time.deltaTime * rotateSpeed);
        }
        else if (currentCameraMode == CameraMode.ROTATE_RIGHT)
        {
            transform.RotateAround(Vector3.zero, Vector2.up, -Time.deltaTime * rotateSpeed);
        }
    }
}
