using UnityEngine;
using System.Collections;

public class TesterSceneController : MonoBehaviour {

    Transform giraffeObject;
    public Transform locationSpawn;
    public Transform defaultGiraffe;

	// Use this for initialization
	void Awake ()
    {
        SetupMainGiraffe();
    }

    void SetupMainGiraffe()
    {
        GameObject mainGiraffe = GameObject.FindWithTag("Player");
        // For testing, if there is no giraffe from the generator in the previous scene, use the default
        if(mainGiraffe)
        {
            giraffeObject = mainGiraffe.transform;
        }
        else
        {
            defaultGiraffe.gameObject.SetActive(true);
            giraffeObject = defaultGiraffe;
        }
        giraffeObject.position = locationSpawn.position;
        giraffeObject.rotation = Quaternion.identity;

        giraffeObject.gameObject.AddComponent<GiraffeController>();
        CharacterController charController = giraffeObject.gameObject.AddComponent<CharacterController>();
        charController.height = giraffeObject.GetComponent<GiraffeInfo>().GetHeight();
        charController.radius = giraffeObject.GetComponent<GiraffeInfo>().GetWidth();

        // Add an animator if the giraffe doesn't have one already
        Animator a = giraffeObject.GetComponent<Animator>();
        if(!a)
        {
            a = giraffeObject.gameObject.AddComponent<Animator>();
        }
        a.runtimeAnimatorController = Resources.Load("Animation/Giraffe") as RuntimeAnimatorController;
    }
}
