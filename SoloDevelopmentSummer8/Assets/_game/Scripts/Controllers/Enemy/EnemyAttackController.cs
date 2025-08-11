using System;
using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.Controllers.Enemy
{
    public class EnemyAttackController : MonoBehaviour, IController
    {
        [SerializeField] protected EnemyStats _stats;
        protected EnemyAnimationController _animator;
        protected EnemyMovementController _movement;
        protected bool _isAttacking;
        protected int _damage;
        

        private void Awake()
        {
            _animator = GetComponentInChildren<EnemyAnimationController>();
            _movement = GetComponent<EnemyMovementController>();
            _animator.AttackClimax += AttackClimax;
            _animator.CompleteAttack += AttackComplete;
        }

       


        public virtual void Handle()
        {
            
            if (Vector3.Distance(transform.position, PlayerBrain.Instance.transform.position) <= _stats.Range)
            {
                if(_isAttacking)
                    return;
                _isAttacking = true;
                _movement.IsMoving = false;
                _animator.Attack();
            }
        }
        
        public virtual void AttackClimax()
        {
            
            if (Vector3.Distance(transform.position, PlayerBrain.Instance.transform.position) <= _stats.Range)
            {
                //need global game multiplier
                PlayerBrain.Instance.Health.TakeDamage(_damage);
            }
        }
        
        public virtual void AttackComplete()
        {
            _movement.IsMoving = true;
            _isAttacking = false;
        }

        public void IncreaseScale(float roundScaler)
        {
          _damage = (int)(roundScaler * _stats.Damage);
        }
    }
}
