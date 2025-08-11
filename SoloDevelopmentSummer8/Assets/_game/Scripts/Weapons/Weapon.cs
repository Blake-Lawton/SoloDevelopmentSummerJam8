using _game.Scripts.Controllers;
using _game.Scripts.Data.WeaponData;
using UnityEngine;

namespace _game.Scripts.Weapons
{
    public abstract class Weapon<T> : WeaponBase  where T : WeaponData
    {
        [SerializeField] protected T _data;
        protected PlayerBrain _player;

        [SerializeField]protected float _currentCooldown;

        public override void Tick()
        {
            _currentCooldown -= Time.deltaTime;

            if (_currentCooldown <= 0)
            {
              
                FireWeapon();
                _currentCooldown = _player.Stats.CalculateCooldown(_player.Stats.CalculateCooldown(_data.Cooldown));
            }
        }

        protected abstract void FireWeapon();

        public override void Equipped(PlayerBrain brain)
        {
            _player = brain;
        }
    }
}
