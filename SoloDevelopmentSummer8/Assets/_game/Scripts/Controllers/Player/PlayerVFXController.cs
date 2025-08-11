using System;
using DG.Tweening;
using UnityEngine;

namespace _game.Scripts.Controllers.Player
{
    public class PlayerVFXController : MonoBehaviour
    {

        private HealthController _healthController;
        [SerializeField] private Material _material;
        [SerializeField] private float _fadeDuration = 0.2f;

        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _healthController.OnHealthChanged += TakeHit;
        }

        public void TakeHit(float damage)
        {
            // Start with full alpha
            Color c = _material.GetColor("_FlashColor");
            c.a = 1f;
            _material.SetColor("_FlashColor", c);

            // Tween alpha from 1 to 0
            DOTween.To(() => _material.GetColor("_FlashColor").a,
                alpha =>
                {
                    Color col = _material.GetColor("_FlashColor");
                    col.a = alpha;
                    _material.SetColor("_FlashColor", col);
                },
                0f,
                _fadeDuration
            );
        }
    }
}
