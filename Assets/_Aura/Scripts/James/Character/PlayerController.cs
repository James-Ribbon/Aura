using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private EntityHealth playerHealth;
    [SerializeField] private LineRenderer lineRenderer;

    private void Start()
    {
        
    }

    public Transform friendHolder;
}
