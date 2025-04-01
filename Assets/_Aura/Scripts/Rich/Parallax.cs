using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private Vector3 startPos;

    public float scrollSpeed = 2f;
    public float parallaxEffect;

    public bool isVertical = false;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        startPos = transform.localPosition;

        if(isVertical)
            length = GetComponent<SpriteRenderer>().bounds.size.y; //Sprite Renderer doesn't care about local space bounds, if parent is rotated 90 degrees x = y
        else
            length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime * parallaxEffect);

        if (transform.localPosition.x > startPos.x + length)
        {
            transform.localPosition = new Vector3(startPos.x, startPos.y, startPos.z);
        }
    }
}