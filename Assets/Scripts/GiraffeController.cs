using UnityEngine;
using System.Collections;

public class GiraffeController : MonoBehaviour {

    Animator animator;
    Rigidbody rb;
    CharacterController charController;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();

    }

    public float x;
    public float y;
    public float z;

    float jumpTime = 0;
    bool goingUp = false;
    void Update () {
        //float verticalAxis = Input.GetAxis("Vertical");
        //float horizontalAxis = Input.GetAxis("Horizontal");
        //if (verticalAxis != 0 || horizontalAxis != 0)
        //{
        //    animator.SetBool("Walking", true);
        //    //rb.AddForce(-transform.right * verticalAxis * 10);
        //    //rb.AddForce(transform.forward * horizontalAxis * 10);
        //    charController.SimpleMove(-transform.right * verticalAxis * 10);
        //    charController.SimpleMove(transform.forward * horizontalAxis * 10);

        //}
        //else
        //{
        //    animator.SetBool("Walking", false);
        //}

        float speed = 20.0F;
        float jumpSpeed = 40.0F;
        float maxJumpTime = 0.5f;
        float minJumpTime = 0.1f;

        float rotateSpeed = 3.0f;
        float gravity = 1000.0f;

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);


        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        Vector3 moveDirection = forward * curSpeed;
        //moveDirection = transform.TransformDirection(moveDirection);

        if (Input.GetButton("Jump"))
        {
            if (charController.isGrounded && Input.GetButtonDown("Jump"))
            {
                jumpTime = 0;
                goingUp = true;
            }
            else if(jumpTime < maxJumpTime && goingUp)
            {
                jumpTime += Time.deltaTime;
            }
            else if(jumpTime >= maxJumpTime)
            {
                goingUp = false;
            }
        }
        else
        {
            if(goingUp && jumpTime < minJumpTime)
            {
                jumpTime += Time.deltaTime;
            }
            else
            {
                goingUp = false;
                jumpTime = 0;
            }
        }

        if(goingUp)
        {
            //print((((1 - jumpTime / maxJumpTime) / 2.0f) + 0.5f));
            moveDirection.y = jumpSpeed * (((1 - jumpTime/maxJumpTime)/2.0f)+0.5f);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        charController.Move(moveDirection * Time.deltaTime);

        if (charController.velocity.magnitude > 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
