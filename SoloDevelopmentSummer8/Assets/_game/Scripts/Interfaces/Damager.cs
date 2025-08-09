using _game.Scripts.Controllers;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Data;
using _game.Scripts.Data.WeaponData;
using UnityEngine;

namespace _game.Scripts.Interfaces
{
    public abstract class Damager<T> : MonoBehaviour where T : WeaponData
    {
        protected T _data;
        private PlayerStatsController _stats;


        private int CalculatedDamage()
        {
            
            //calculate damage from player stats
            return (int)(_data.Damage * _stats.DamageMulti);
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HealthController health))
            {
                health.TakeDamage(CalculatedDamage());
            }
        }

        public virtual void SetUp(PlayerStatsController playerStats, T weaponData)
        {
            _stats = playerStats;
            _data = weaponData;
        }
    }
}
