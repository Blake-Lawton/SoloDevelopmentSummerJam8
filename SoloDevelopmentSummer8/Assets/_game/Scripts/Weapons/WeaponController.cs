using System;
using System.Collections.Generic;
using _game.Scripts.Controllers;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Data.WeaponData;
using _game.Scripts.Global;
using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.Weapons
{
    public class WeaponController : MonoBehaviour, IController
    {
        [SerializeField] private List<WeaponBase> _weapons;
        [SerializeField] private Transform _weaponsPocket;
        private PlayerBrain _brain;

        private void Awake()
        {
            _brain = GetComponent<PlayerBrain>();
            GlobalEvents.OnEnterChaos += ResetCoolDownOfAllWeapons;
            foreach (var weapon in _weapons)
            {
                weapon.Equipped(_brain);
            }
        }

        public void Handle()
        {
            foreach (var weapon in _weapons)
                weapon.Tick();
        }

        public void EquippedWeapon(Weapon<WeaponData> weapon)
        {
            _weapons.Add(weapon);
            weapon.Equipped(_brain);
        }

        public void ResetCoolDownOfAllWeapons()
        {
            foreach (var weapon in _weapons)
                weapon.ResetCoolDown();
            
        }
    }
}
