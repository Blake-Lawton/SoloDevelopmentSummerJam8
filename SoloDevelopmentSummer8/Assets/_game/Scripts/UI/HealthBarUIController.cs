using System;
using _game.Scripts.Controllers;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.UI
{
    public class HealthBarUIController : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBarPrefab;
        [SerializeField] private Vector3 _healthBarOffset = new Vector3(0,2.5f,0);
        private HealthBar _healthBar;
        private HealthController _healthController;


        private void Awake()
        {
            _healthController = GetComponent<HealthController>();
            _healthController.OnHealthChanged += HealthChange;
            _healthController.OnDeath += HideHealthBarUI;
        }

        private void HideHealthBarUI()
        {
            _healthBar.Show(false);
        }

        private void Start()
        {
            _healthBar = Instantiate(_healthBarPrefab, ScreenSpaceCanvas.Instance.transform);
            _healthBar.SetUp(transform, _healthBarOffset);
        }

        private void HealthChange(float percentage)
        {
            _healthBar.UpdateHealthBar(percentage);
        }

        public void ResetFresh()
        {
            _healthBar.Show(true);
            HealthChange(1);
        }
    }
}
