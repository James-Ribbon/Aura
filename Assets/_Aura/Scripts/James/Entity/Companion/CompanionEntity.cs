using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionEntity : Entity
{
    public Transform followTarget;
    public float followDistance = 1f;

    private bool isAttached;

    protected override void Awake()
    {
        base.Awake();
        movement.Init(originPoint);
    }

    private void Update()
    {
        if(followTarget == null)
        {
            movement.UpdateMovement(Vector2.zero, moveSpeed, smoothFactor);
        }
        else
        {
            movement.UpdateMovement(followTarget.position, moveSpeed, smoothFactor);
        }

            /*if(!isAttached && followTarget != null)
            {
                float dist = Vector2.Distance(transform.position, followTarget.position);
                Vector2 targetPos = dist > followDistance ? followTarget.position : transform.position;

                movement.UpdateMovement(targetPos, moveSpeed, smoothFactor);
            }*/

        UpdateAnimation();
    }

    void FollowPlayer()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            followTarget = collision.transform.GetComponent<PlayerController>().friendHolder;

            isAttached = true;
            SetAttached(true);
            moveSpeed = 7f;

            transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        }
    }
}
