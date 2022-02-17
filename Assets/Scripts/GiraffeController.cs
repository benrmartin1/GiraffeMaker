using UnityEngine;
using System.Collections;

// Responsible for allowing player control of giraffe, including jumps and movement
public class GiraffeController : MonoBehaviour
{

    Animator animator;
    Rigidbody rb;
    CharacterController charController;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
    }

    float jumpTime = 0;
    bool goingUp = false;

    void Update()
    {
        float speed = 20.0F;
        float jumpSpeed = 60.0F;
        float maxJumpTime = 0.5f;
        float minJumpTime = 0.1f;

        float rotateSpeed = 3.0f;
        float gravity = 1200.0f;

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);


        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        Vector3 moveDirection = forward * curSpeed;

        // Super simple stupid algo to jump
        // Allows short jumps by tapping jump or long jumps by holding jump, up until maxJumpTime
        // This block calculates whether we are "goingUp" based on how long jump input has been held
        if (Input.GetButton("Jump"))
        {
            if (charController.isGrounded && Input.GetButtonDown("Jump"))
            {
                jumpTime = 0;
                goingUp = true;
            }
			// Track how long this jump has lasted
            else if (jumpTime < maxJumpTime && goingUp)
            {
                jumpTime += Time.deltaTime;
            }
			// Once you've jumped the maxJumpTime, force jump to end
            else if (jumpTime >= maxJumpTime)
            {
                goingUp = false;
            }
        }
        else
        {
            // Keep jumping until minJumpTime even if not holding jump, to force a minimum jump height
            if (goingUp && jumpTime < minJumpTime)
            {
                jumpTime += Time.deltaTime;
            }
			// End the jump after letting go of jump key and minJumpTime
            else
            {
                goingUp = false;
                jumpTime = 0;
            }
        }

        if (goingUp)
        {
            moveDirection.y = jumpSpeed * (((1 - jumpTime / maxJumpTime) / 2.0f) + 0.5f);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        charController.Move(moveDirection * Time.deltaTime);

        // Animate the player while it is moving in any direction
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
