using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private float knockbackDuration = 0.5f;
    [SerializeField] private float knockbackRadius = 2f;

    [SerializeField] private LayerMask _targetLayers;

    [SerializeField] private bool isKnockingBack = false;

    public void Activate()
    {
        Debug.Log("Knockback activated!");

        if (!isKnockingBack)
        {
            Debug.Log("Knockback Happening!");


            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, knockbackRadius, _targetLayers);

            if (colliders.Length == 0)
            {
                Debug.Log("No colliders found!");
            }
            else
            {
                Debug.Log($"Found {colliders.Length} colliders!");
                foreach (Collider2D collider in colliders)
                {
                    Debug.Log($"Collider: {collider.name}");
                }
            }

            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

                    Debug.Log($"Enemy Hit: {rb.transform.name}");

                    if (rb != null)
                    {
                        Vector2 direction = (collider.transform.position - transform.position).normalized;
                        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
                        
                    }
                }
            }
            //StartCoroutine(ResetKnockback());
            isKnockingBack = true;
        }


    }

    private void OnDisable()
    {
        isKnockingBack = false;
    }
/*
    IEnumerator ResetKnockback()
    {
        Debug.Log("Knockback Restting!");

        yield return new WaitForSeconds(knockbackDuration);
        isKnockingBack = false;
    }
*/
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, knockbackRadius);
    }
}