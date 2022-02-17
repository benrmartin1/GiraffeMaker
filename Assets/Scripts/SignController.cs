using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SignController : MonoBehaviour {

    public Animator anim;
    public Text nameText;

    public void DropSign()
    {
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
