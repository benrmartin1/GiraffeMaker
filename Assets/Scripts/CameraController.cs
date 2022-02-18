using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Controlls the camera during the generation scene. There are multiple camera modes to view the
// giraffe at different angles. Camera lerps between modes using coroutines
public class CameraController : MonoBehaviour
{
    public List<Material> skyboxMats;
    private int currentSkybox = 0;

    private Camera theCamera;

    public enum CameraMode
    {
        FLAT,
        THREE_D,
        ROTATE_LEFT,
        ROTATE_RIGHT,
        ANGLE,
        ANGLE_ROTATE
    };

    private CameraMode currentCameraMode = CameraMode.FLAT;

    private Vector3 flatCameraPosition = new Vector3(0, 4, -12);
    private Vector3 flatCameraRotation = new Vector3(0, 0, 0);
    private Vector3 angleCameraPosition = new Vector3(0, 12, -12);
    private Vector3 angleCameraRotation = new Vector3(35, 0, 0);
    private Vector3 threeDCameraPosition = new Vector3(0, 6, -12);
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
        theCamera.orthographic = true;
        SetSolidBackground();

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(flatCameraPosition, flatCameraRotation, CameraMode.FLAT));
    }

    public void SetCameraMode3D()
    {
        theCamera.orthographic = false;
        SetSkybox();

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(threeDCameraPosition, threeDCameraRotation, CameraMode.THREE_D));
    }

    public void SetCameraModeRotateLeft()
    {
        theCamera.orthographic = false;
        SetSkybox();

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(threeDCameraPosition, threeDCameraRotation, CameraMode.ROTATE_LEFT));
    }

    public void SetCameraModeRotateRight()
    {
        theCamera.orthographic = false;
        SetSkybox();

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(threeDCameraPosition, threeDCameraRotation, CameraMode.ROTATE_RIGHT));
    }

    public void SetCameraModeAngle()
    {
        theCamera.orthographic = false;
        SetSkybox();

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(angleCameraPosition, angleCameraRotation, CameraMode.ANGLE));
    }

    public void SetCameraModeAngleRotate()
    {
        theCamera.orthographic = false;
        SetSkybox();

        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveCamera(angleCameraPosition, angleCameraRotation, CameraMode.ANGLE_ROTATE));
    }

    IEnumerator MoveCamera(Vector3 position, Vector3 rotation, CameraMode newCameraMode)
    {
        Vector3 startPosition = theCamera.transform.position;
        Quaternion startRotation = theCamera.transform.rotation;
        for (float i = 0f; i < 1; i += Time.deltaTime / 0.5f)
        {
            theCamera.transform.position = Vector3.Lerp(startPosition, position, i);
            theCamera.transform.rotation = Quaternion.Slerp(startRotation, Quaternion.Euler(rotation), i);
            yield return null;
        }
        currentCameraMode = newCameraMode;

    }

    public void SetSolidBackground()
    {
        theCamera.clearFlags = CameraClearFlags.SolidColor;
    }

    public void SetSkybox()
    {
        if (theCamera.clearFlags != CameraClearFlags.Skybox)
        {
            theCamera.clearFlags = CameraClearFlags.Skybox;
            RenderSettings.skybox = skyboxMats[currentSkybox];
        }
    }

    // Cycle between the different skyboxes.
    // Cycle backwards when backward=true, forwards when backwards=false
    public void CycleSkybox(bool backward = false)
    {
        if (theCamera.clearFlags == CameraClearFlags.Skybox)
        {
            currentSkybox += backward ? -1 : 1;
            // Make sure we stay in range, and not negative
            currentSkybox = (currentSkybox % skyboxMats.Count + skyboxMats.Count) % skyboxMats.Count; 
            print(currentSkybox);
            RenderSettings.skybox = skyboxMats[currentSkybox];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Minus))
        {
            rotateSpeed -= Time.deltaTime * 20;
            if (rotateSpeed < 1)
            {
                rotateSpeed = 1;
            }
        }
        else if (Input.GetKey(KeyCode.Equals)) //actually the plus key
        {
            rotateSpeed += Time.deltaTime * 20;
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
