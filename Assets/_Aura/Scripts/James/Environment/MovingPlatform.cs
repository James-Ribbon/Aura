using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points;
    int targetPoint = 0;
    public float speed = 1;

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[targetPoint].position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, points[targetPoint].position) < 0.1f)
        {
            targetPoint++;

            if (targetPoint >= points.Count)
            {
                targetPoint = 0;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
