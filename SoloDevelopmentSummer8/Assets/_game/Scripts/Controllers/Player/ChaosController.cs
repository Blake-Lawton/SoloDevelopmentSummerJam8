using System;
using System.Collections;
using _game.Scripts.Controllers.Enemy;
using _game.Scripts.Data;
using _game.Scripts.Global;
using _game.Scripts.Interfaces;
using _game.Scripts.Managers;
using _game.Scripts.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace _game.Scripts.Controllers.Player
{
    public class ChaosController : MonoBehaviour, IController
    {
        [SerializeField] private Image _chaosFillBar;
        [SerializeField] private ParticleSystem _chaosAuraVFX;
        [SerializeField] private Volume _volume;
        [SerializeField] private UVScroller _barScroller;

        [SerializeField] private AudioSource _chaosStartAudio;
        [SerializeField] private AudioSource _mainAudio;
        private AnimationController _animation;
        private Vignette _vignette;
        private PlayerStatsController _stats;
        private HealthController _health;
        [SerializeField,ReadOnly] private float _currentChaos = 0;
        [SerializeField,ReadOnly] private float _maxChaos = 30;

        private float _chaosDamageTickTime = 0;
      
        private bool _consumingChaos = false;
        private bool _chaosMode;
        
        
        public float CurrentChaos => _currentChaos;
        public float MaxChaos => _maxChaos;
        public bool ChaosMode => _chaosMode;
        public bool CanGainChaos { get; set; }

        private void Awake()
        {
            _animation = GetComponentInChildren<AnimationController>();
            _stats = GetComponent<PlayerStatsController>();
            _health = GetComponent<HealthController>();
           
            _chaosAuraVFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if(_volume.profile.TryGet(out Vignette vignette))
                _vignette = vignette;
            
            _animation.OnChaosModeExit += ExitChaosModeAnimation;
            GlobalEvents.OnEnemyDeath += GenerateChaos;

            CanGainChaos = true;
        }

     


        public void Handle()
        {
            if(GameStateManager.Instance.GameState == GameState.Upgrades)
                return;
            
            _chaosFillBar.fillAmount = _currentChaos / _maxChaos;
            
            if (_chaosMode)
            {
                ChaosModeConsume();
                return;
            }
               
            if (Input.GetKeyDown(KeyCode.Space) && _currentChaos > 0)
                StartChaos();
            

            if(_consumingChaos)
               ConsumeChaos();
            
            if (Input.GetKeyUp(KeyCode.Space))
               StopChaos();
            
            
          
        }

        private void ChaosModeConsume()
        {
            _currentChaos -= Time.deltaTime * 3;
            if (_currentChaos <= 0)
            {
                ExitChaosMode();
            }
               
        }

        private void ConsumeChaos()
        {
            _currentChaos -= Time.deltaTime;
            if (_currentChaos <= 0)
                StopChaos();
            
           
            _chaosDamageTickTime += Time.deltaTime;

            if (_chaosDamageTickTime >= 1 && UpgradeManager.Instance.ChaosUpgrade)
            {
                _health.TakeDamage(2);
                _chaosDamageTickTime = 0;
            }
            
            
        }
        
        private void GenerateChaos(EnemyBrain enemy)
        {
            if(_health.IsDead)
                return;
            int chaosToCollect = _stats.CalculateChaosMulti();
            EndGameScoreCard.Instance.ChaosCollect((int)(chaosToCollect * GameStateManager.Instance.RoundScaler));
            
            if(_chaosMode )
                return;
            
            if(!CanGainChaos)
                return;
            
            _currentChaos += chaosToCollect;
            
            if (_currentChaos >= _maxChaos)
            {
                StartCoroutine(EnterChaosMode_CO());
            }
        }


        private IEnumerator EnterChaosMode_CO()
        {
            if(_chaosMode)
                yield break;
            
            StopChaos();
            _animation.EnterChaosMode();
            EnterChaosMode();
            _chaosStartAudio.Play();
            GlobalEvents.EnterChaos();
            DOTween.To(
                () => Time.timeScale,         // Getter
                x => Time.timeScale = (float)x,      // Setter
                .5f,                         // Target value
                1.5f                                 // Duration
            );
            DOTween.To(
                () => _vignette.intensity.value,         // Getter
                x => _vignette.intensity.value = (float)x,      // Setter
                .7,                         // Target value
                1.5f                                 // Duration
            );
            _mainAudio.DOPitch(.7f, 1.5f).OnComplete(() =>
            {
                _mainAudio.DOPitch(1f, .2f);
            });
            yield return new WaitForSeconds(2);
            
            _chaosAuraVFX.Play();
            _chaosAuraVFX.gameObject.transform.localScale = Vector3.one * 4;
        }
        
        private void ExitChaosModeAnimation()
        {
            _barScroller.SetScrollSpeed(.5f);
            Time.timeScale = 1f;
            DOTween.To(
                () => _vignette.intensity.value,         // Getter
                x => _vignette.intensity.value = (float)x,      // Setter
                .5f,                         // Target value
                .5f                                 // Duration
            );
           
        }
        private void EnterChaosMode()
        {
          
            
            _chaosMode = true;
            CanGainChaos = false;
           
            _stats.IncreaseSpeedMulti(1f);
            _stats.IncreaseDamageMulti(1.5f);
            _stats.IncreaseCooldownMulti(.6f);
            
            
        }

        private void ExitChaosMode()
        {
            _stats.DecreaseSpeedMulti(1f);
            _stats.DecreaseDamageMulti(1.5f);
            _stats.DecreaseCooldownMulti(.6f);
            _chaosAuraVFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            _chaosAuraVFX.gameObject.transform.localScale = Vector3.one * 3;
            _chaosMode = false;
            DOTween.To(
                () => _vignette.intensity.value,         // Getter
                x => _vignette.intensity.value = x,      // Setter
                0f,                         // Target value
                1f                                 // Duration
            );
            GlobalEvents.EndRound();
        }
        private void StartChaos()
        {
            if (UpgradeManager.Instance.ChaosUpgrade)
            {
                _stats.IncreaseSpeedMulti(.7f);
                _stats.IncreaseDamageMulti(.7f);
                _stats.IncreaseCooldownMulti(.4f);
            }
            else
            {
                _stats.IncreaseSpeedMulti(.5f);
                _stats.IncreaseDamageMulti(.5f);
                _stats.IncreaseCooldownMulti(.3f);
            }
          
            _chaosAuraVFX.Play();
            _consumingChaos = true;
            _barScroller.SetScrollSpeed(0.5f);
        }

    
        private void StopChaos()
        {
            if (_consumingChaos)
            {
                if (UpgradeManager.Instance.ChaosUpgrade)
                {
                    _stats.DecreaseSpeedMulti(.7f);
                    _stats.DecreaseDamageMulti(.7f);
                    _stats.DecreaseCooldownMulti(.4f);
                }
                else
                {
                    _stats.DecreaseSpeedMulti(.5f);
                    _stats.DecreaseDamageMulti(.5f);
                    _stats.DecreaseCooldownMulti(.3f);
                }
               
                _chaosAuraVFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                _consumingChaos = false;
                _barScroller.SetScrollSpeed(0.2f);
            }
            
        }

        public void ResetFresh()
        {
            _currentChaos = 0;
            _chaosMode = false;
            _consumingChaos = false;
            _chaosAuraVFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            _vignette.intensity.value = 0f;
            _barScroller.SetScrollSpeed(0.2f);
        }
    }
}
