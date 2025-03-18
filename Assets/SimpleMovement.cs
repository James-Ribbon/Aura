using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private Rigidbody2D rig;

    public float strength = 5f;

    private Vector2 moveInput;
    /**** input settings ****/
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
        
    }
}
