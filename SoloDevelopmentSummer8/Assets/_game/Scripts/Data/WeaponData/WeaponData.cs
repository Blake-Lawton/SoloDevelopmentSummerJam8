using _game.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace _game.Scripts.Data.WeaponData
{
    
    public class WeaponData : ScriptableObject
    {
    
        [SerializeField] private WeaponBase _weaponPrefab;
       
        [SerializeField] private float _cooldown;
        [FormerlySerializedAs("_damage")] [SerializeField] private float _damageScaler;
        
        public WeaponBase WeaponPrefab => _weaponPrefab;
        public float Cooldown => _cooldown;
        public float DamageScaler => _damageScaler;
    }
    
   
}

