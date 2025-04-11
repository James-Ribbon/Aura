using System.Collections;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    public bool jumping = false;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isRunning;

    private Vector3 defaultScale;
    [SerializeField] private Vector2 currentVelocity;

    [SerializeField] private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("JumpTrigger");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //DialogueManager.Instance.ShowDialogue(transform, "Jumping!", 2f);
        }

        anim.SetBool("isJumping", !isGrounded);


        currentVelocity = rb.velocity;

    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = isRunning ? runSpeed : moveSpeed;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);        

        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            transform.localScale = defaultScale;
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
        }

        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);

    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
