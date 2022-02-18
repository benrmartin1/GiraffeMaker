using UnityEngine;
using System.Collections;

public class TesterSceneController : MonoBehaviour {

    Transform giraffeObject;
    public Transform locationSpawn;
    public Transform defaultGiraffe;
    public AudioSource music;

	// Use this for initialization
	void Awake ()
    {
        SetupMainGiraffe();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Mute/unmute
            if (music.isPlaying)
            {
                music.Stop();
            }
            else
            {
                music.Play();
            }
        }
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
            

        // Add a character controller and animator if the giraffe doesn't have one already.
        // The default giraffe has them, so this prevents them being added twice
        CharacterController charController = giraffeObject.gameObject.GetComponent<CharacterController>();
        if(!charController)
        {
            charController = giraffeObject.gameObject.AddComponent<CharacterController>();
            charController.height = giraffeObject.GetComponent<GiraffeInfo>().GetHeight();
            charController.radius = giraffeObject.GetComponent<GiraffeInfo>().GetWidth();
        }

        Animator anim = giraffeObject.GetComponent<Animator>();
        if(!anim)
        {
            anim = giraffeObject.gameObject.AddComponent<Animator>();
        }
        anim.runtimeAnimatorController = Resources.Load("GiraffeAnimation/Giraffe") as RuntimeAnimatorController;
    }
}
