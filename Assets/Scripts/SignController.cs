using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SignController : MonoBehaviour {

    public Transform nameSign;
    public Animator anim;
    public Text nameText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DropSign()
    {
        // might not need this
        //nameText.text = GiraffeMaker.curentGiraffeName;
        anim.SetTrigger("SignDrop");
    }

    public void ChangeSign()
    {
        DropSign();
    }

    public void SetNameToCurrentName()
    {
        nameText.text = GiraffeMaker.curentGiraffeName;
    }
}
