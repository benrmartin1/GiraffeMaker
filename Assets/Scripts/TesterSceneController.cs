using UnityEngine;
using System.Collections;

public class TesterSceneController : MonoBehaviour {

    Transform giraffeObject;
    public Transform locationSpawn;

	// Use this for initialization
	void Start ()
    {
        SetupMainGiraffe();
    }

    void SetupMainGiraffe()
    {
        giraffeObject = GameObject.FindWithTag("Giraffe").transform;
        giraffeObject.position = locationSpawn.position;

        giraffeObject.gameObject.AddComponent<GiraffeController>();
        CharacterController charController = giraffeObject.gameObject.AddComponent<CharacterController>();
        charController.height = 5.97f;
        charController.radius = 1.19f;

        Animator a = giraffeObject.gameObject.AddComponent<Animator>();
        a.runtimeAnimatorController = Resources.Load("Animation/Giraffe") as RuntimeAnimatorController;
    }
}
