using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed = 5f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal * speed, body.linearVelocity.y);
        body.linearVelocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(body.linearVelocity.y) < 0.01f)
        {
            body.AddForce(new Vector2(0, 300f)); // Jump force
        }
    }
}
