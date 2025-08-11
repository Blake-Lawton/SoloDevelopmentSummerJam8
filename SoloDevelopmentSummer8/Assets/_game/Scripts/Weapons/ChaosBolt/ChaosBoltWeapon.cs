
using _game.Scripts.Data.WeaponData;
using UnityEngine;
using UnityEngine.Serialization;

namespace _game.Scripts.Weapons.ChaosBolt
{
    public class ChaosBoltWeapon: Weapon<ProjectileData> 
    {
        [SerializeField] private ChaosBoltProjectile _boltProjectile;
        [SerializeField] private Transform _firePoint;
        
        protected override void FireWeapon()
        {
            ChaosBoltProjectile boltProjectileInstance = Instantiate(_boltProjectile, _firePoint.position, _firePoint.rotation);
            boltProjectileInstance.SetUp(_player.Stats, _data);
        }

        public override void ResetCoolDown()
        {
            _currentCooldown = 0;
        }
    }
}
