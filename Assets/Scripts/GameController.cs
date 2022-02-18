using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject platform;
    public Transform startingText;
    public SignController signController;
    public GameObject particles;

    private CameraController camController;
    private Transform giraffe;
    // True until the first giraffe is generated
    private bool firstGenerate = true;
    // True if already swapped to the secret name list
    private bool swappedNames = false;


    public void SetGiraffe(Transform newGiraffe)
    {
        giraffe = newGiraffe;
    }

    // Use this for initialization
    void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            camController.SetCameraModeFlat();
            platform.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            camController.SetCameraMode3D();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            camController.SetCameraModeRotateLeft();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            camController.SetCameraModeRotateRight();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            camController.SetCameraModeAngle();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            camController.SetCameraModeAngleRotate();
            platform.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Go to next skybox
            camController.CycleSkybox();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Go to previous skybox
            camController.CycleSkybox(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateGiraffe();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            // Giraffe selected. Load test scene with giraffe
            SceneManager.LoadScene("Playground");
        }
        else if (Input.GetKey(KeyCode.F3) && Input.GetKey(KeyCode.F5) && !swappedNames)
        {
            // Secret input, start particle effect and swap name list
            particles.SetActive(true);
            RandomNamer.SwapNameList("Names2.txt");
            swappedNames = true;
        }
    }

    void GenerateGiraffe()
    {
        // Generate a new giraffe, and remove the current one if there is one
        if (giraffe != null)
        {
            Destroy(giraffe.gameObject);
        }

        giraffe = GetComponent<GiraffeMaker>().GenerateGiraffe();

        // Do the fancy sign drop down animation
        if (firstGenerate)
        {
            firstGenerate = false;
            StartCoroutine(startingText.GetComponent<StartingTextController>().Shrink());
        }
        signController.DropSign();
    }
}