using UnityEngine;

public class Floater : Enemy
{
    /*
    -- Enemy floats around world area as if random
    -- If player gets too close it will track the player and "attack"
    -- Attack would be a tendril moving towards the player/contact with player
     */

    [Header("A Transform for the enemy to float around")]
    public Transform originPoint;

    [Header("Float Area Settings")]
    public float floatRadius = 2f;
    public float moveSpeed = 1f;
    [Tooltip("How often to pick a new position (in seconds)")]
    public float changePositionInterval = 2f;

    [Header("Movement Smoothing")]
    [Range(0.1f, 0.9f)]
    public float smoothFactor = 0.5f;

    [SerializeField] private GameObject target;
    private Vector2 currentTargetPosition;
    private float timeSinceLastChange;

    private bool playerSighted = false;
    private float timePlayerIsUnseen = 0;
    [SerializeField] float timeToWait = 3f;

    private Vector2 velocity;

    private Vector2 currentPosition;
    private Vector2 newPosition;

    protected override void Init()
    {
        base.Init();
    }   

    void Start()
    {
        currentState = EnemyState.Idle;

        if (originPoint == null)
        {
            originPoint = transform.parent.transform;
        }

        currentTargetPosition = GetRandomPointInCircle();
        timeSinceLastChange = changePositionInterval;
    }

    protected override void Update()
    {
        base.Update();

        currentPosition = transform.position;

        //Move towards the target position
        newPosition = Vector2.SmoothDamp(
            currentPosition,
            currentTargetPosition,
            ref velocity,
            smoothFactor,
            moveSpeed
        );

        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

    }

    #region Idle Mechanics
    protected override void IdleState()
    {
        base.IdleState();

        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= changePositionInterval)
        {
            currentTargetPosition = GetRandomPointInCircle();
            timeSinceLastChange = 0f;
        }   
    }

    Vector2 GetRandomPointInCircle()
    {
        Vector2 randomPoint = Random.insideUnitCircle * floatRadius;
        return (Vector2)originPoint.position + randomPoint;
    }
    #endregion

    #region Chase Mechanics

    protected override void ChasingState()
    {
        base.ChasingState();

        currentTargetPosition = target.transform.position;

        if (!playerSighted)
        {
            timePlayerIsUnseen += Time.deltaTime;
        }

        if(timePlayerIsUnseen >= timeToWait)
        {
            target = null;
            currentTargetPosition = GetRandomPointInCircle();
            currentState = EnemyState.Idle;
            timePlayerIsUnseen = 0f;
        }
    }

    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
#if UNITY_EDITOR
            Debug.Log("BLUHHHHH");
#endif
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
#if UNITY_EDITOR
            Debug.Log("Tiggered Chase Mode");
#endif

            target = collision.gameObject;

            currentState = EnemyState.Chasing;

            playerSighted = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
#if UNITY_EDITOR
            Debug.Log("Chasing Player");
#endif

            Vector2 direction = collision.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            /*if(currentState == EnemyState.Chasing)
                currentTargetPosition = collision.transform.position;*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
#if UNITY_EDITOR
            Debug.Log("Lost Sight of Player");
#endif

            playerSighted = false;
        }
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
