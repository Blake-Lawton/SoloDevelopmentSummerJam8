using System;
using _game.Scripts.Data;
using _game.Scripts.Interfaces;
using _game.Scripts.Managers;
using UnityEngine;

namespace _game.Scripts.Controllers.Player
{
    public class HealthController : MonoBehaviour, IController 
    {
        [SerializeField] private int _currentHealth;
        [SerializeField] private int _maxHealth;

        private bool _isDead;
        public event Action<float> OnHealthChanged;
        public event Action OnDeath;
        
       public bool IsDead => _isDead;

        public void TakeDamage(int damage)
        {
            if(GameStateManager.Instance.GameState != GameState.InRound && TryGetComponent(out PlayerBrain playerBrain))
                return;
            
          
            _currentHealth -= damage;
            OnHealthChanged?.Invoke(GetHealthPercentage());
            if (_currentHealth <= 0 && !_isDead)
            {
                _isDead = true;
                Died();
            }
        }

        public void TakeMaxDamage()
        {
            TakeDamage(_maxHealth);
        }

        public void Handle()
        {
            
        }

        private float GetHealthPercentage()
        {
            return (float)_currentHealth / _maxHealth;
        }

        private void Died()
        {
            OnDeath?.Invoke(); 
        }

        public void IncreaseHealth(float roundScaler)
        {
            _maxHealth = (int)(_maxHealth * roundScaler);
            _currentHealth = _maxHealth;
        }

        public void ResetFresh()
        {
            
            _isDead = false;
            _currentHealth = _maxHealth;
        }

        public void Heal(float percentage)
        {
            if(_currentHealth >= _maxHealth)
                return;
            _currentHealth += (int)(_maxHealth * percentage);
        }

        public int GetPercentage(float percentage)
        {
            return (int)(_maxHealth * percentage);
        }

        public void TakeDamageUpgrade(int percentage)
        {
            _currentHealth -= percentage;
            if (_currentHealth <= 0)
                _currentHealth = 1;
            
            OnHealthChanged?.Invoke(GetHealthPercentage());
            
        }
    }
}
