using System;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Data;
using _game.Scripts.Managers;
using _game.Scripts.UI;
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
         private ChaosController _chaos;
         private HealthBarUIController _healthBarUI;
         public static PlayerBrain Instance;
        
        public WeaponController Weapons => _weapons;
        public PlayerStatsController Stats => _stats;
        public HealthController Health => _health;
        
        private void Awake()
        {
            Instance = this;
            _health = GetComponent<HealthController>();
            _movement = GetComponent<MovementController>();
            _animation = GetComponentInChildren<AnimationController>();
            _weapons = GetComponent<WeaponController>();
            _stats = GetComponent<PlayerStatsController>();
            _chaos = GetComponent<ChaosController>();
            _healthBarUI = GetComponent<HealthBarUIController>();
            _health.OnDeath += EndGame;
        }

        private void EndGame()
        {
            EndGameScoreCard.Instance.EndGame();
            GameStateManager.Instance.EndGame();
            UpgradeManager.Instance.ResetFresh();
            _chaos.ResetFresh();
            EndGameScoreCard.Instance.ResetFresh();
        }

        public void StartGame()
        {
            _health.ResetFresh(); 
            _stats.ResetFresh();
            _healthBarUI.ResetFresh();
        }

        private void Update()
        {
            _animation.Handle();
            
            if (GameStateManager.Instance.GameState != GameState.InRound) return;
            
            _movement.Handle();
            _health.Handle();
            
            _weapons.Handle();
            _stats.Handle();
            _chaos.Handle();


        }
        
        
    }
}
