using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField]private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _cooldown = 2;
    
    public int Health => _health;
    public float Speed => _speed;
    public int Damage => _damage;
    public float Range => _range;
    
    public float Cooldown => _cooldown;
}
