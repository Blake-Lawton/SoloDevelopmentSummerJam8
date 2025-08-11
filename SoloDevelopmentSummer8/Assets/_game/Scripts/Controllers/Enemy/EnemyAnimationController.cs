using System;
using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.Controllers.Enemy
{
    public class EnemyAnimationController : MonoBehaviour, IController
    {
        private Animator _animator;
        public event Action AttackClimax;
        public event Action CompleteAttack;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        public void AttackClimaxAnim()
        {
            AttackClimax?.Invoke();
        }

        public void AttackCompleteAnim()
        {
            CompleteAttack?.Invoke();
        }

        public void Handle()
        {
            
        }

        public void Death()
        {
            _animator.SetTrigger("Death");
        }
    }
}
