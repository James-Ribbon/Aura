using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Text soulText;
    public TMP_Text OrbText;

    private void OnEnable()
    {
        EventManager.SoulCoreCollected += EventManagerOnSoulCollected;
        EventManager.RecallOrbCollected += UpdateNumberOfRecallOrbs;

    }

    private void OnDisable()
    {
        EventManager.SoulCoreCollected -= EventManagerOnSoulCollected;
        EventManager.RecallOrbCollected -= UpdateNumberOfRecallOrbs;
    }

    private void EventManagerOnSoulCollected()
    {
        Debug.Log("You have collected a soul shard. Updating UI Display");
    }

    void UpdateNumberOfRecallOrbs()
    {
        if(OrbText != null) 
        {
            //OrbText.text = GameManager.Instance.orb
        }
    }
}