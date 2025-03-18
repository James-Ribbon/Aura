using UnityEngine;

public class UIController : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.SoulCollected += EventManagerOnSoulCollected;
    }

    private void OnDisable()
    {
        EventManager.SoulCollected -= EventManagerOnSoulCollected;
    }

    private void EventManagerOnSoulCollected()
    {
        Debug.Log("You have collected a soul shard. Updating UI Display");
    }
}
