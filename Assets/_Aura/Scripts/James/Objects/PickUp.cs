using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class PickUp : MonoBehaviour
{
    public PickUpType pickUpType;

    [Tooltip("Leave value at -1 if not required")]
    public int itemID = -1;

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
            case PickUpType.Soul:

                EventManager.OnSoulCollected();
                
                break;

            case PickUpType.Key:

                EventManager.OnKeyCollected(itemID);

                break;
        }

        Destroy(this.gameObject);
    }
}

public enum PickUpType
{
    Soul,
    Key
}