using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public TMP_Text SoulText;
    public TMP_Text OrbText;

    public int mCores = 0;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventManager.SoulCoreCollected += EventManagerOnSoulCollected;
        //EventManager.RecallOrbCollected += UpdateNumberOfRecallOrbs;

    }

    private void OnDisable()
    {
        EventManager.SoulCoreCollected -= EventManagerOnSoulCollected;
        //EventManager.RecallOrbCollected -= UpdateNumberOfRecallOrbs;
    }

    private void EventManagerOnSoulCollected()
    {
        Debug.Log("You have collected a soul shard. Updating UI Display");
        mCores += 1;
        string soulText = $"Memory Cores: {mCores.ToString()}";
        SoulText.text = soulText;
    }

    void UpdateNumberOfRecallOrbs()
    {
        if(OrbText != null) 
        {
            //OrbText.text = GameManager.Instance.orb
        }
    }

    public void UpdateNumberOfRecallOrbs(int val)
    {
        if (OrbText != null)
        {
            //OrbText.text = GameManager.Instance.orb
            string orbText = $"Recall Orbs: {val.ToString()}";
            OrbText.text = orbText;
        }
    }
}