using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject platform;

    private CameraController camController;
    private Transform giraffe;

    public void SetGiraffe(Transform newGiraffe)
    {
        giraffe = newGiraffe;
    }

    // Use this for initialization
    void Start () {
        camController = Camera.main.GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            camController.SetCameraModeFlat();
            platform.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camController.SetCameraMode3D();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            camController.SetCameraModeRotateLeft();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            camController.SetCameraModeRotateRight();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            camController.SetCameraModeAngle();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            camController.SetCameraModeAngleRotate();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            camController.CycleSkybox(true);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            camController.CycleSkybox(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (giraffe != null)
            {
                Destroy(giraffe.gameObject);
            }
            giraffe = GetComponent<GiraffeMaker>().GenerateGiraffe();
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            // Giraffe selected. Load test scene
            SceneManager.LoadScene("tester");
        }
    }
}
