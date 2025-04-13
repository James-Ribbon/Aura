using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amyloid : Entity
{
    [SerializeField] private int doorID = -1;
    [SerializeField] private bool isLocked = false;



    public bool activate = false;

    private void Awake()
    {
        if (doorID != -1)
            isLocked = true;
    }

    private void Start()
    {
        foreach (Collider2D orb in circleColliders)
        {
            Rigidbody2D rb = orb.gameObject.GetComponent<Rigidbody2D>();

            //rb.gravityScale = 1f;

            rb.isKinematic = true;
        }
    }

    private void OnEnable()
    {
        if (doorID != -1)
        {
            EventManager.KeyCollected += OnKeyCollected;
        }
    }

    private void OnDisable()
    {
        if (doorID != -1)
        {
            EventManager.KeyCollected -= OnKeyCollected;
#if UNITY_EDITOR
            Debug.Log("Door Disabled");
#endif
        }
    }

    private void Update()
    {
        if (activate)
        {            
            StartCoroutine(RemoveWall());
            activate = false;
        }
    }
    IEnumerator RemoveWall()
    {
        foreach(Collider2D orb in circleColliders)
        {
            Rigidbody2D rb = orb.gameObject.GetComponent<Rigidbody2D>();

            //rb.gravityScale = 1f;
            
            rb.isKinematic = false;

            float rand = Random.Range(0.01f, 0.3f);

            yield return new WaitForSeconds(rand);
        }

        Debug.Log("Destroying Amyloid Complete");

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    private void OnKeyCollected(int key)
    {
        if (key == doorID)
        {
            isLocked = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(!isLocked)
            {
                activate = true;
            }
        }
    }
}
