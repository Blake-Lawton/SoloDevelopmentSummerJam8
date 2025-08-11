using System;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Global;
using UnityEngine;

namespace _game.Scripts.Controllers.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        private HealthController _health;
        private EnemyMovementController _movement;
        private EnemyAnimationController _animation;
        private EnemyAttackController _attack;
        public HealthController Health => _health;
        

        private void Awake()
        {
            _health = GetComponent<HealthController>();
            _movement = GetComponent<EnemyMovementController>();
            _animation = GetComponentInChildren<EnemyAnimationController>();
            _attack = GetComponent<EnemyAttackController>();
            _health.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            _animation.Death();
            _movement.Death();
            GlobalEvents.EnemyDied(this);
        }

        private void Update()
        {
            _health.Handle();
            _movement.Handle();
            _animation.Handle();
            _attack.Handle();
        }

        public void SetUp(float roundScaler)
        {
            _health.IncreaseHealth(roundScaler);
            _movement.IncreaseSpeed(roundScaler);
            _attack.IncreaseScale(roundScaler);
        }
    }
}
