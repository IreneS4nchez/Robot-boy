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
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        transform.Translate(Vector2.right * h * speed * Time.deltaTime);

        if (h != 0) transform.localScale = new Vector3(Mathf.Sign(h), 1, 1);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("IsJumping", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        anim.SetFloat("Speed", isGrounded ? (h != 0 ? (Input.GetKey(KeyCode.LeftShift) ? 2f : 1f) : 0f) : 0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
        anim.SetBool("IsJumping", false);
    }

    void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }
}
