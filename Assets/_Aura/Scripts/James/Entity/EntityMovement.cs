using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private Transform originPoint;

    public float floatRadius = 2f;
    public float positionChangeInterval = 2f;

    private Vector2 currentTargetPosition;
    private Vector2 velocity;
    private float timeSinceLastChange;

    public void Init(Transform origin)
    {
        originPoint = origin;
        currentTargetPosition = GetRandomPointInCircle();
        timeSinceLastChange = 0f;
    }

    public void UpdateMovement(Vector2 targetPos, float moveSpeed, float smoothFactor)
    {
        if(targetPos == Vector2.zero)
        {
            timeSinceLastChange += Time.deltaTime;

            if (timeSinceLastChange >= positionChangeInterval)
            {
                currentTargetPosition = GetRandomPointInCircle();
                timeSinceLastChange = 0f;
            }
        }
        else
        {
            currentTargetPosition = targetPos;
        }

        Vector2 newPosition = Vector2.SmoothDamp(
                transform.position,
                currentTargetPosition,
                ref velocity,
                smoothFactor,
                moveSpeed
            );

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    Vector2 GetRandomPointInCircle()
    {
        Vector2 randomPoint = Random.insideUnitCircle * floatRadius;
        return (Vector2)originPoint.position + randomPoint;
    }

    void OnDrawGizmosSelected()
    {
        if (originPoint != null)
        {
            Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
            Gizmos.DrawWireSphere(originPoint.position, floatRadius);
        }

        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
