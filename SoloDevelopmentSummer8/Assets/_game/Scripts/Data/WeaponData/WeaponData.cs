using _game.Scripts.Weapons;
using UnityEngine;

namespace _game.Scripts.Data.WeaponData
{
    
    public class WeaponData : ScriptableObject
    {
    
        [SerializeField] private WeaponBase _weaponPrefab;
       
        [SerializeField] private float _cooldown;
        [SerializeField] private float _damage;
        
        public WeaponBase WeaponPrefab => _weaponPrefab;
        public float Cooldown => _cooldown;
        public float Damage => _damage;
    }
    
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/Projectile Data")]
    public class ProjectileData : WeaponData
    {
        [SerializeField] private float _speed;
        public float Speed => _speed;
    }
}

