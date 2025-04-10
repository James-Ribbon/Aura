using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public ObjectPool dialogueBoxPool;

    public Canvas worldCanvas;

    [Header("Dialogue Settings")]
    [SerializeField] private int maxActiveDialogues = 3;
    [SerializeField] private float textSpeed = 0.05f;
    [SerializeField] private float dialogueDuration = 3f;
    public float textBoxOffset = 1.5f;

    [Space(10)]

    private GameObject dialogueBox;
    private Transform currentTarget;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
    }

    private void Update()
    {
        if (dialogueBox != null && dialogueBox.activeSelf && currentTarget != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(
                currentTarget.position + Vector3.up * textBoxOffset);

            dialogueBox.transform.position = screenPosition;
        }
    }

    public void ShowDialogue(Transform target, string message, float dialogueDuration)
    {
         StartCoroutine(ShowDialogueCoroutine(target, message, dialogueDuration));
    }

    public void HideDialogue()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }

        // You could trigger an "OnDialogueHidden" event here
    }

    IEnumerator ShowDialogueCoroutine(Transform target, string message, float dialogueDuration)
    {
        HideDialogue();

        dialogueBox = dialogueBoxPool.GetPooledObject();
        //dialogueBox.transform.SetParent(worldCanvas.transform, false);

        if (dialogueBox != null)
        {
            currentTarget = target;
            TMP_Text textComponent = dialogueBox.GetComponentInChildren<TMP_Text>();
            textComponent.text = message;
            dialogueBox.SetActive(true);
        }
        yield return new WaitForSeconds(dialogueDuration);

        HideDialogue();
    }
}