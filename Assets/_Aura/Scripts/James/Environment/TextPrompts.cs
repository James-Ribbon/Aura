using UnityEngine;

public class TextPrompts : MonoBehaviour
{
    public string textToDisplay;
    public float timeToStay = 3f;

    private bool textIsActive = false;
    private float timer = 0f;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(textIsActive)
        {
            timer += Time.deltaTime;

            if(timer > timeToStay ) 
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !textIsActive)
        {
            DialogueManager.Instance.ShowDialogue(collision.transform, textToDisplay, timeToStay);
            textIsActive = true;
        }
    }
}
