using System;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Data.WeaponData;
using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.Weapons.ChaosBolt
{
    public class ChaosBoltProjectile : Damager<ProjectileData>
    {
        private float _currentLife;

        private void Start()
        {
            _currentLife = _data.Lifetime;
        }

        private void Update()
        {
            transform.position += transform.forward * _data.Speed * Time.deltaTime;

            _currentLife -= Time.deltaTime;
            
            if(_currentLife <= 0)
                Destroy(gameObject);
        }

       
    }
}
