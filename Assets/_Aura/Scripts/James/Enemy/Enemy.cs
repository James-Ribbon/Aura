using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyType enemyType;

    [SerializeField] private int _health;

    public void Init()
    {
        if(enemyType != null)
        {
            _health = enemyType.enemyHealth;
        }
    }
}
