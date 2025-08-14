using _game.Scripts.Data;
using _game.Scripts.Data.PlayerData;
using _game.Scripts.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Controllers.Player
{
    public class PlayerStatsController : MonoBehaviour, IController
    {
        

        [SerializeField] private PlayerStatsData _data;
        private float _damageMulti = 1;
        private float _speedMulti = 1;
        [SerializeField]private float _cooldownMulti = 0;
        private float _chaosMulti = 1;


        public float DamageMulti => _damageMulti;
        public float SpeedMulti => _speedMulti;
        public float CooldownMulti => _cooldownMulti;
        public float ChaosMulti => _chaosMulti;
        public void Handle()
        {
          
        }

        public void IncreaseSpeedMulti(float speedMulit)
        {
            _speedMulti += speedMulit;
        }

        public void DecreaseSpeedMulti(float speedMulit)
        {
            _speedMulti -= speedMulit;
        }

        public void IncreaseDamageMulti(float damageMulti)
        {
            _damageMulti += damageMulti;
        }

        public void DecreaseDamageMulti(float damageMulti)
        {
            _damageMulti -= damageMulti;
        }

        
        public float GetSpeed()
        {
            return _data.Speed * _speedMulti;
        }

        public int CalculateDamage(float dataDamageScaler)
        {
            return (int)((_data.Damage * dataDamageScaler) * _damageMulti);
        }

        [Button]
        public void IncreaseCooldownMulti(float cooldownMulti)
        {
            
            _cooldownMulti += cooldownMulti;
            _cooldownMulti = Mathf.Clamp(_cooldownMulti, 0, .7f);
        }

        public void DecreaseCooldownMulti(float cooldownMulti)
        {
            _cooldownMulti -= cooldownMulti;
        }
        public float CalculateCooldown(float coolDown)
        {
            var calculatedCooldown = coolDown - (coolDown * _cooldownMulti);
            return calculatedCooldown;
        }

        public void IncreaseChaosMulti(float chaos)
        {
            _chaosMulti += chaos;
        }

        public void DecreaseChaosMulti(float chaos)
        {
            _chaosMulti -= chaos;
        }

        public int CalculateChaosMulti()
        {
            return (int)(1f * _chaosMulti);
        }

        public void ResetFresh()
        {
            _damageMulti = 1;
            _speedMulti = 1;
            _cooldownMulti = 0;
            _chaosMulti = 1;
        }
    }
}
