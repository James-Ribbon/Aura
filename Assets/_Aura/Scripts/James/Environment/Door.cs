using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider2D doorCollider;

    [SerializeField] private Animator anim;

    [Tooltip("If NO key is required, leave Door ID as -1")]
    [SerializeField] private int doorID = -1;
    
    [SerializeField] private bool isOpen = false;

    [SerializeField] private bool isLocked = false;

    [SerializeField] private bool oneTime = false;

    private void Awake()
    {
        doorCollider = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponent<Animator>();

        if (doorID != -1)
            isLocked = true;
    }

    private void OnEnable()
    {
        if(doorID != -1)
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

    private void OnKeyCollected(int key)
    {
        if(key == doorID)
        {
            isLocked = false;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isLocked)
            Destroy(this.gameObject);
    }*/

    void ToggleDoor(bool toggle)
    {
        this.enabled = toggle;
        anim.enabled = toggle;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isLocked)
        {
            doorCollider.enabled = false;
            anim.SetTrigger("Toggle");

            //if (oneTime)
                //ToggleDoor(false);
        }
        else if(collision.gameObject.tag == "Player" && isLocked)
        {
#if UNITY_EDITOR
            Debug.Log("Deal Player Damage");
#endif
        }
        //else
            //anim.SetTrigger("Toggle");

    }
}
