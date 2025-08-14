using _game.Scripts.Controllers;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Data;
using _game.Scripts.Data.WeaponData;
using _game.Scripts.Managers;
using UnityEngine;

namespace _game.Scripts.Interfaces
{
    public abstract class Damager<T> : MonoBehaviour where T : WeaponData
    {
        protected T _data;
        private PlayerStatsController _stats;


        private int CalculatedDamage()
        {
            return _stats.CalculateDamage(_data.DamageScaler);
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HealthController health))
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && !health.IsDead)
                {
                    UpgradeManager.Instance.RollOnHit();
                    health.TakeDamage(CalculatedDamage());
                }
                    
            }
        }

        public virtual void SetUp(PlayerStatsController playerStats, T weaponData)
        {
            _stats = playerStats;
            _data = weaponData;
        }
    }
}
