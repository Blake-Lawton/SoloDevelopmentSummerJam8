using UnityEngine;

namespace _game.Scripts.Data
{
    [CreateAssetMenu(fileName = "DamagerData", menuName = "Scriptable Objects/Damager Data")]
    public class DamagerData : ScriptableObject
    {
        [SerializeField] private int _damage;
    
        public int Damage => _damage;
    }
}
