using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Transform originPoint;
    public float moveSpeed = 1f;
    public float smoothFactor = 0.5f;
    public CircleCollider2D[] circleColliders;
    public GameObject spriteObject;

    [Header("Animation Settings")]
    public float rotationSpeed = 60f;
    public float scaleFrequency = 4f;
    public float scaleAmplitude = 0.025f;

    protected Animator anim;
    protected Vector3 originalSpriteScale;
    protected EntityMovement movement;

    protected virtual void Awake()
    {
        movement = GetComponent<EntityMovement>();
        anim = spriteObject.GetComponent<Animator>();
        originalSpriteScale = spriteObject.transform.localScale;

        if (originPoint == null)
        {
            originPoint = transform.parent.transform;
        }
    }

    protected virtual void UpdateAnimation()
    {
        spriteObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        float oscillation = Mathf.Sin(Time.time * scaleFrequency) * scaleAmplitude;

        float currentScale = 1 + oscillation;

        spriteObject.transform.localScale = originalSpriteScale * currentScale;
    }

    public virtual void SetAttached(bool attached)
    {
        foreach(var collider in circleColliders)
        {
            collider.enabled = !attached;
        }
    }
}