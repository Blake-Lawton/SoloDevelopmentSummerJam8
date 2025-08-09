using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField]private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    
    public int Health => _health;
    public float Speed => _speed;
    public int Damage => _damage;
}
