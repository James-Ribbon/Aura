using UnityEngine;

public class RichMove : MonoBehaviour
{
    public float speed;
    private float move;

    public Parallax[] layers;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move = Input.GetAxis("Horizontal") * speed;

        rb.velocity = new Vector2(move, rb.velocity.y);
        
        //foreach(Parallax layer in layers)
        //{
        //    layer.speed = move;
        //}
    }
}
