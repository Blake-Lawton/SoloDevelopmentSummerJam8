using System;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Weapons;
using UnityEngine;

namespace _game.Scripts.Controllers
{
    public class PlayerBrain : MonoBehaviour
    {
         private HealthController _health;
         private MovementController _movement;
         private AnimationController _animation;
         private WeaponController _weapons;
         private PlayerStatsController _stats;
         
        public static PlayerBrain Instance;
        
        public WeaponController Weapons => _weapons;
        public PlayerStatsController Stats => _stats;
        private void Awake()
        {
            Instance = this;
            _health = GetComponent<HealthController>();
            _movement = GetComponent<MovementController>();
            _animation = GetComponent<AnimationController>();
            _weapons = GetComponent<WeaponController>();
            _stats = GetComponent<PlayerStatsController>();
        }


        private void Update()
        {
            _movement.Handle();
            _health.Handle();
            _animation.Handle();
            _weapons.Handle();
            _stats.Handle();
        }
    }
}
