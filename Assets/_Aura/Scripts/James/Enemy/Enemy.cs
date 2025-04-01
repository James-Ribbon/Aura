using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    [SerializeField] private int _health;

    public virtual void Init()
    {
        if(enemyType != null)
        {
            _health = enemyType.enemyHealth;
        }
    }
}
