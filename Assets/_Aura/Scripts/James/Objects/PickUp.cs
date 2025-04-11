using UnityEngine;

public class PickUp : MonoBehaviour
{
    public PickUpType pickUpType;

    [Tooltip("Leave value at -1 if not required")]
    public int itemID = -1;

    [Header("Turn on/off pick up animations")]
    [SerializeField] private bool enableAnimations;

    [Header("Animation Settings")]
    public float amplitude = .025f;
    public float frequency = 4;
    public float rotationSpeed = 160;

    private float startY;
    private Vector3 currentPos;

    private void Start()
    {
        startY = transform.position.y;
        currentPos = transform.position;
    }

    private void Update()
    {
        if (enableAnimations)
        {
            currentPos.y = startY + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = currentPos;
            transform.Rotate(Vector3.one, rotationSpeed * Time.deltaTime);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PickUpLogic();
        }
    }

    private void PickUpLogic()
    {
        switch (pickUpType) 
        {
            case PickUpType.SoulCore:

                EventManager.OnSoulCoreCollected();

                //Destroy(gameObject);
                
                break;

            case PickUpType.Key:

                EventManager.OnKeyCollected(itemID);

                break;

            case PickUpType.RecallOrb:

                EventManager.OnRecallOrbCollected();

                break;
        }

        Destroy(this.gameObject);
    }
}

public enum PickUpType
{
    SoulCore,
    Key,
    RecallOrb
}