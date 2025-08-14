using System;
using _game.Scripts.Controllers.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _game.Scripts.Managers
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField]private PlayerStatsController _playerStatsController;
        [SerializeField]private HealthController _health;
        
        [Header("Stats")]
        [SerializeField]private TMP_Text _damageText;
        [SerializeField]private TMP_Text _speedText;
        [SerializeField]private TMP_Text _cooldownText;
        
        
        [Header("Descriptions")]
        [SerializeField]private TMP_Text _goodDescription;
        [SerializeField]private TMP_Text _neutralDescription;
        [SerializeField]private TMP_Text _evilDescription;

        private int _goodOptionsIndex;
        private int _neutralOptionsIndex;
        private int _evilOptionsIndex;
        
        private bool _onHitBuffs = false;
        private bool _onHitHeals = false;

        private bool _chaosUpgrade = false;
        private bool _doubleProjectile;
        public bool DoubleProjectile => _doubleProjectile;
        public bool ChaosUpgrade => _chaosUpgrade;
        
        public static UpgradeManager Instance;

        
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            _damageText.text = "Damage: " + ((_playerStatsController.DamageMulti - 1) * 100f).ToString("0") + "%";
            _speedText.text = "Speed: " + ((_playerStatsController.SpeedMulti - 1) * 100f).ToString("0") + "%";
            _cooldownText.text = "Cooldown: " + (_playerStatsController.CooldownMulti * 100f).ToString("0") + "%";
            
        }

        [Button]
        public void ChaoticGoodOption()
        {

            switch (_goodOptionsIndex)
            {
                case 0:
                    _onHitBuffs = true;
                    _goodDescription.text = "Have a 5% chance to heal on hit.";
                    break;
                case 1:
                    _onHitHeals = true;
                    _goodDescription.text = "Heal 50% health.";
                    break;
                default:
                    _health.Heal(0.50f);
                    break;
            }

            _goodOptionsIndex++;

        }

        public void RollOnHit()
        {
            if(!_onHitBuffs)
                return;
            var Roll = Random.Range(0, 101);

            
            if (Roll <= 4)
            {
                var buff = Random.Range(0, 3);
                if(_onHitHeals)
                    _health.Heal(0.05f);
                switch (buff)
                {
                    case 0:
                        _playerStatsController.IncreaseDamageMulti(0.05f);
                        break;
                    case 1:
                        _playerStatsController.IncreaseCooldownMulti(0.01f);
                        break;
                    case 2:
                        _playerStatsController.IncreaseSpeedMulti(0.03f);
                        break;
                }
            }
        }
        
        [Button]
        public void NeutralOption()
        {
            
            switch (_neutralOptionsIndex)
            {
                
                case 0:
                    var damage = Random.Range(0, 2);
                    if (damage == 1)
                        _playerStatsController.IncreaseDamageMulti(0.5f);
                    else
                        _playerStatsController.DecreaseDamageMulti(0.5f);
                    
                    _neutralDescription.text = "Randomly gain between -20% speed or 20% speed.";
                    break;
                case 1:
                    var speed = Random.Range(0, 2);
                    if (speed == 1)
                        _playerStatsController.IncreaseSpeedMulti(0.2f);
                    else
                        _playerStatsController.DecreaseSpeedMulti(0.2f);
                    
                    _neutralDescription.text = "Randomly gain -10% cooldown or 10% cooldown.";
                    break;
                case 2:
                    var cooldown = Random.Range(0, 2);
                    if (cooldown == 1)
                        _playerStatsController.IncreaseCooldownMulti(0.1f);
                    else
                        _playerStatsController.DecreaseCooldownMulti(0.1f);
                    
                   
                    _neutralDescription.text = "Randomly gain -50% damage or 50% damage.";
                    break;
            }

            _neutralOptionsIndex++;
            if (_neutralOptionsIndex >= 3)
                _neutralOptionsIndex = 0;
        }


        [Button]
        public void EvilOption()
        {
            switch (_evilOptionsIndex)
            {
                case 0:
                    _evilDescription.text = "You spawn 1 more projectile but so do enemies.";
                    _chaosUpgrade = true;
                    break;
                case 1:
                    _doubleProjectile = true;
                    _evilDescription.text = "Take 25% damage but gain 15 % damage. This can not kill you.";
                    break;
                default:
                    _health.TakeDamageUpgrade(_health.GetPercentage(0.25f));
                    _playerStatsController.IncreaseDamageMulti(0.15f);
                    break;
            }
            
            _evilOptionsIndex++;
        }
        
        public void ResetFresh()
        {
            _neutralDescription.text = "Randomly gain -50% damage or 50% damage.";
            _goodDescription.text = "5% chance to increase 5% damage, 3% movespeed, or 1% cooldown on hit.";
            _evilDescription.text =
                "Gain more damage,  speed, and cooldown when using chaos, but take also take damage while consuming.";
            _onHitBuffs = false;
            _onHitHeals = false;
            _goodOptionsIndex = 0;
            _neutralOptionsIndex = 0;
            _evilOptionsIndex = 0;
            _chaosUpgrade = false;
            _doubleProjectile = false;
        }
    }
    
    
  
   
}
