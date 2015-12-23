using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float maxSpeed = 100f;
    public float jumpForce = 700f;
    public bool flipForLeft = true;
    public Transform groundCheck;
    public LayerMask whatIsGround;


    int facing = 1;
    bool grounded = false;
    float groundRadius = 0.5f;
    bool hasReleasedJumpButton = true;


    Rigidbody2D playerRigidBody;
    Animator playerAnimator;



    // Use this for initialization
    void Start()
    {
        playerRigidBody = this.GetComponent<Rigidbody2D>();
        playerAnimator = this.GetComponent<Animator>();

    }

    void Update()
    {

        if (grounded && Input.GetAxis("Jump") > 0 && hasReleasedJumpButton)
        {
            playerAnimator.SetBool("grounded", false);
            playerRigidBody.AddForce(new Vector3(0, jumpForce));
            hasReleasedJumpButton = false;
        }

        if (Input.GetAxis("Jump") == 0)
        {
            hasReleasedJumpButton = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        playerAnimator.SetBool("grounded", grounded);

        playerAnimator.SetFloat("vSpeed", playerRigidBody.velocity.y);

        // Get state of X movement input (analog stick / buttons)
        float moveX = Input.GetAxisRaw("Horizontal");

        // Apply the movement as a velocity
        playerRigidBody.velocity = new Vector2(moveX * maxSpeed * Time.fixedDeltaTime, playerRigidBody.velocity.y);
        
        // Update the animator
        if (moveX == 0)
        {
            playerAnimator.SetBool("moving", false);

        }
        else
        {
            playerAnimator.SetBool("moving", true);

            if (moveX > 0)
            {
                // if changing direction & sprite is likely flipped, flip it back.
                if (facing < 0 && flipForLeft) Flip();

                facing = 1;

            }
            else
            {
                // If changing direction & sprite should flip, flip it. Flip it good.
                if (facing > 0 && flipForLeft) Flip();

                facing = -1;
            }
            playerAnimator.SetInteger("direction", facing);
        }
        
    }


    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= - 1;
        transform.localScale = theScale;
    }
}
