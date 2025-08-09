using UnityEngine;

namespace _game.Scripts.Data.PlayerData
{
    [CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Scriptable Objects/Player Stats Data")]
    public class PlayerStatsData : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private int _speed;
        [SerializeField] private float _cooldown;
        
        public int Health => _health;
        public int Damage => _damage;
        public float Speed => _speed;
        public float Cooldown => _cooldown;


    }
}
