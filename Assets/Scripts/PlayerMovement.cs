using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed = 5f;
    private Animator anim;
    private bool grounded;


    private void Awake()
    {
        //Grab references for components and animation
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal * speed, body.linearVelocity.y);
        body.linearVelocity = movement;

        // Flip the player sprite based on movement direction
        if (moveHorizontal > 0.01f)
        {
            // Face right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveHorizontal < -0.01f)
        {
            // Face left
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(body.linearVelocity.y) < 0.01f && grounded)
        {
            Jump();
        }

        // Update animation parameters
        anim.SetBool("walk", moveHorizontal != 0);
        anim.SetBool("grounded", grounded);

    }

    private void Jump()
    {
        body.AddForce(new Vector2(0, 300f)); // Jump force
        anim.SetTrigger("Jump"); // Trigger jump animation
        grounded = false; // Set grounded to false when jumping
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, 0); // Reset vertical velocity on landing
        }
    }
}
