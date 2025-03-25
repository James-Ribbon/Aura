using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private float startPos;

    public float scrollSpeed = 2f;
    public float parallaxEffect;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime * parallaxEffect);

        Debug.Log($"{gameObject.name}: {transform.position.x} :: {startPos+length}");

        if (transform.position.x > startPos + length)
        {
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}