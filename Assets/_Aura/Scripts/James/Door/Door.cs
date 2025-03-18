using UnityEngine;

public class Door : MonoBehaviour
{
    [Tooltip("If NO key is required, leave Door ID as -1")]
    [SerializeField] private int doorID = -1;
    
    [SerializeField] private bool isOpen = false;

    [SerializeField] private bool isLocked = false;

    private void Awake()
    {
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
        }
    }

    private void OnKeyCollected(int key)
    {
        if(key == doorID)
        {
            isLocked = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isLocked)
            Destroy(this.gameObject);
    }
}
