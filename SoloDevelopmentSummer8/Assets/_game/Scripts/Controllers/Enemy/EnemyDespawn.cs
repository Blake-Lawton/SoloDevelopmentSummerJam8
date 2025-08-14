using System;
using _game.Scripts.Controllers.Player;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _game.Scripts.Controllers.Enemy
{
    public class EnemyDespawn : MonoBehaviour
    {
        private HealthController _healthController;

        [SerializeField] private Material _basicMaterial;
        
        [SerializeField] private Material _dissolveMaterial;
        [SerializeField] private Renderer[] _renderers;

        
        private Material _sharedBasicMaterial;
        private Material _sharedDissolveMaterial;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _healthController.OnDeath += Despawn;
            _healthController.OnDeath += RecordKill;
            _healthController.OnHealthChanged += TakeHit;
            // Create one shared instance of the dissolve material
            _sharedDissolveMaterial = new Material(_dissolveMaterial);
            _sharedBasicMaterial = new Material(_basicMaterial);
            
            foreach (var renderer in _renderers)
            {
                Material[] mats = renderer.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = _sharedBasicMaterial;
                }
                renderer.materials = mats;
            }
        }

        private void RecordKill()
        {
            EndGameScoreCard.Instance.EnemyKill();
        }


        public void Despawn()
        {
            foreach (var renderer in _renderers)
            {
                Material[] mats = renderer.materials;
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = _sharedDissolveMaterial; // same instance for all
                }
                renderer.materials = mats;
            }

            _sharedDissolveMaterial
                .DOFloat(0f, "_CutOffHeight", 2f)
                .From(3f)
                .SetEase(Ease.Linear).SetDelay(5).onComplete= () =>
            {
                Destroy(gameObject);
            };
        }
        
        public void TakeHit(float percentage)
        {
            // Kill any running tweens so hits don't overlap
            _sharedBasicMaterial.DOKill();

            // Get the original color (assuming your shader uses _Color)
            Color originalColor = _sharedBasicMaterial.color;
    
            // Flash to red, then back
            _sharedBasicMaterial
                .DOColor(Color.red, "_BaseColor", 0.2f)
                .OnComplete(() =>
                {
                    _sharedBasicMaterial
                        .DOColor(originalColor, "_BaseColor", 0.2f);
                });
        }
    }
}
