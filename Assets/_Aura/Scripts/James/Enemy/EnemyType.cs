using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/EnemyTypeScriptableObject", order = 1)]
public class EnemyType : ScriptableObject
{
    public GameObject enemyPrefab;
    public int enemyHealth;
    
}
