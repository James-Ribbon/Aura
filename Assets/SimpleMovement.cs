using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    /*private Rigidbody2D rig;

    public float strength = 5f;

    private Vector2 moveInput;
    *//**** input settings ****//*
    private string vert = "Vertical";
    private string hor = "Horizontal";

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput.x = Input.GetAxis(hor);
        moveInput.y = Input.GetAxis(vert);

        Vector2 moveDirection = Vector2.right * moveInput;

        moveDirection *= strength;

        
        rig.AddForce(moveDirection);
        
    }*/

    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = isRunning ? runSpeed : moveSpeed;
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
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
