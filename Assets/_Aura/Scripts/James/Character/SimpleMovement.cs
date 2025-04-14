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
    public bool isFacingRight;


    private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isRunning;

    private Vector3 defaultScale;
    [SerializeField] private Vector2 currentVelocity;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator anim;

    //Remove this in future and set up player controller correctly 
    [SerializeField] private CameraFollow cameraFollow;

    [Header("Wall Jump")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallJumpForce = 10f;
    [SerializeField] private float wallJumpHorizontalForce = 5f;
    [SerializeField] private float wallJumpTime = 0.01f;

    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private bool canWallJump;
    [SerializeField] private float wallJumpDirection;

    private void Awake()
    {
        isFacingRight = true;
    }

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

        isTouchingWall = Physics2D.Raycast(wallCheck.position, 
                            Vector2.right * transform.localScale.x,
                             wallCheckDistance, wallLayer);

        isWallSliding = isTouchingWall && !isGrounded && rb.velocity.y < 0;

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -2f, float.MaxValue));
            canWallJump = true;
        }
        else
        {
            canWallJump = false;
        }

        if ((isGrounded || canWallJump) && Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            }
            else if (isTouchingWall)
            {
                WallJump();
                //WallJump();
            }

            anim.SetTrigger("JumpTrigger");
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

        if (moveInput > 0 && !isFacingRight)
        {
            transform.localScale = defaultScale;
            isFacingRight = true;
            cameraFollow.PlayerTurn();
            //spriteRenderer.flipX = false;
        }
        else if (moveInput < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
            isFacingRight = false;
            cameraFollow.PlayerTurn();
            //spriteRenderer.flipX = true;
        }

        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);

    }

    private void WallJump()
    {
        canWallJump = false;
        wallJumpDirection = -transform.localScale.x; // Jump away from the wall
        cameraFollow.PlayerTurn();
        isFacingRight = !isFacingRight;
        // Apply forces
        rb.velocity = new Vector2(wallJumpDirection * wallJumpHorizontalForce, wallJumpForce);

        // Optional: Flip the character direction
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        // Prevent immediate re-walljump
        StartCoroutine(ResetWallJump());
    }

    private IEnumerator ResetWallJump()
    {
        yield return new WaitForSeconds(wallJumpTime);
        canWallJump = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position,
                            groundCheck.position + Vector3.down * groundCheckRadius);
        }

        // Draw wall check line (horizontal)
        if (wallCheck != null)
        {
            Gizmos.color = isTouchingWall ? Color.red : Color.blue;
            Vector3 direction = Vector3.right * transform.localScale.x;
            Gizmos.DrawLine(wallCheck.position,
                           wallCheck.position + direction * wallCheckDistance);
        }
    }
}
