using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Responsible for allowing player control of giraffe, including jumps and movement
public class GiraffeController : MonoBehaviour
{

    Text scoreText;
    Text infoText;
    GameObject ammo;

    Animator animator;
    CharacterController charController;

    int score = 0;
    const int unlockJumpScore = 10;
    const int unlockShootScore = 20;

    // Control related vars
    float jumpTime = 0;
    bool goingUp = false;

    const float speed = 20.0F; //20
    const int ammoSpeed = 40;

    const float jumpSpeed = 60.0F;
    const float maxJumpTime = 0.5f;
    const float minJumpTime = 0.1f;

    const float rotateSpeed = 3.0f;
    const float gravity = 1200.0f;

    bool jumpEnabled = false;
    bool shootEnabled = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    void Awake()
    {
        scoreText = GameObject.Find("FoodText").GetComponent<Text>();
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        ammo = Resources.Load("Prefabs/Ammo") as GameObject;
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);


        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        Vector3 moveDirection = forward * curSpeed;

        // Super simple stupid algo to jump
        // Allows short jumps by tapping jump or long jumps by holding jump, up until maxJumpTime
        // This block calculates whether we are "goingUp" based on how long jump input has been held
        if (Input.GetButton("Jump") && jumpEnabled)
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
        animator.SetBool("Walking", charController.velocity.magnitude > 0);

        if (Input.GetKeyDown(KeyCode.F) && shootEnabled)
        {
            // Put the spawned ammo in front of player slightly above (approx. head height)
            Vector3 ammoPos = transform.forward * 3 + transform.position;
            ammoPos.y += 2.5f;
            GameObject newAmmo = Instantiate(ammo, ammoPos, transform.rotation);
            newAmmo.GetComponent<Rigidbody>().velocity = transform.forward * ammoSpeed;
            // Play sound effect
            newAmmo.GetComponent<AudioSource>().Play();
        }
            
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Foods: " + score;

        if (score >= unlockJumpScore && score < unlockShootScore)
        {
            infoText.text = "[Space] to jump. Eat 20 foods to be strong enough to shoot!";
            jumpEnabled = true;
        }
        else if (score >= unlockShootScore)
        {
            infoText.text = "[Space] to jump. [F] to shoot. You got all the food! Giraffes shouldn't eat too much!";
            shootEnabled = true;
        }
    }
}
