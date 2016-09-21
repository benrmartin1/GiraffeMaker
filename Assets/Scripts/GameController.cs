using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject terrain;

    CameraController camController;

    // Use this for initialization
    void Start () {
        camController = Camera.main.GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            camController.SetCameraModeFlat();
            terrain.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camController.SetCameraMode3D();
            terrain.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            camController.SetCameraModeRotateLeft();
            terrain.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            camController.SetCameraModeRotateRight();
            terrain.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            camController.SetCameraModeAngle();
            terrain.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            camController.SetCameraModeAngleRotate();
            terrain.SetActive(true);
        }
    }
}
