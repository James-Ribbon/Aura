using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    [SerializeField] private int _health;

    [SerializeField] protected EnemyState currentState;

    protected virtual void Init()
    {
        if(enemyType != null)
        {
            _health = enemyType.enemyHealth;
        }
    }    

    protected virtual void Update()
    {
        switch (currentState) 
        {
            case EnemyState.Idle:

                IdleState();

                break;

            case EnemyState.Chasing:

                ChasingState();

                break;

            case EnemyState.Attacking:

                AttackingState();

                break;
            case EnemyState.Friendly:

                FriendlyState();

                break;
        }
    }

    protected virtual void IdleState()
    {

    }

    protected virtual void ChasingState()
    {

    }

    protected virtual void AttackingState() 
    {
    
    }

    protected virtual void FriendlyState()
    {

    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Friendly
}