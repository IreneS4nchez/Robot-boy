using UnityEngine;

public class RobotBoy : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        bool runKey = Input.GetKey(KeyCode.LeftShift);
        bool runJoystick = Input.GetKey(KeyCode.JoystickButton8);

        bool isRunning = runKey || runJoystick;
        float speed = isRunning ? runSpeed : walkSpeed;

        transform.Translate(Vector2.right * h * speed * Time.deltaTime);

        if (h != 0)
            transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("IsJumping", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        bool isMoving = h != 0 && isGrounded;

        anim.SetBool("Idle", !isMoving);
        anim.SetBool("SpeedWalk", isMoving && !isRunning);
        anim.SetBool("SpeedRun", isMoving && isRunning);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("IsJumping", false);
        }
    }
}
