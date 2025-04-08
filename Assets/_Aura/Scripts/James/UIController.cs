using UnityEngine;

public class UIController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.SoulCoreCollected += EventManagerOnSoulCollected;
    }

    private void OnDisable()
    {
        EventManager.SoulCoreCollected -= EventManagerOnSoulCollected;
    }

    private void EventManagerOnSoulCollected()
    {
        Debug.Log("You have collected a soul shard. Updating UI Display");
    }
}
