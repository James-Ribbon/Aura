using UnityEngine;

public class ObserverEntity : Entity
{
    public float detectionRadius = 2f;
    public float timeToWait = 3f;

    private GameObject target;
    private bool playerSighted;
    private float timePlayerIsUnseen = 0;

    protected override void Awake()
    {
        base.Awake();
        movement.Init(originPoint);
    }

    private void Update()
    {
        if(target == null)
        {
            movement.UpdateMovement(Vector2.zero, moveSpeed, smoothFactor);
        }
        else
        {
            Chase();
        }

        UpdateAnimation();
    }

    private void Chase()
    {
        movement.UpdateMovement(target.transform.position, moveSpeed, smoothFactor);

        if(!playerSighted)
        {
            timePlayerIsUnseen += Time.deltaTime;

            if (timePlayerIsUnseen >= timeToWait)
            {
                anim.SetTrigger("Idle");
                target = null;
                timePlayerIsUnseen = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
#if UNITY_EDITOR
            Debug.Log("Chasing Player");
#endif
            target = collision.gameObject;
            playerSighted = true;
            anim.SetTrigger("Chase");   //So Not to call it more than once
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            anim.SetTrigger("Attack");
        }
    }
}