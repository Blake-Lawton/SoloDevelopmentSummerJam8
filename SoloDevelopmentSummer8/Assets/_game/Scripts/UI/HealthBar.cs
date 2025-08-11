using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
      
         
        [SerializeField] private float _lerpSpeed = 5f;
        private Vector3 _offset = new Vector3(0, 2f, 0); 
        private Transform _target; 

        [Header("UI References")]
        [SerializeField] private Image _healthFill; 
       
        private Coroutine _healthLerpCoroutine;

        private Camera _mainCam;

        public void SetUp(Transform target, Vector3 offset)
        {
            _mainCam = Camera.main;
            _target = target;
            _offset = offset;
        }

        private void LateUpdate()
        {
          
            Vector3 screenPos = _mainCam.WorldToScreenPoint(_target.position + _offset);
            transform.position = screenPos;
            
        }

        

        public void UpdateHealthBar(float percentage)
        {
            if(!isActiveAndEnabled)
                return;
            
            if (_healthLerpCoroutine != null)
                StopCoroutine(_healthLerpCoroutine);

            
            _healthLerpCoroutine = StartCoroutine(LerpHealthBar(percentage));
        }

        private IEnumerator LerpHealthBar(float targetFill)
        {
            float startFill = _healthFill.fillAmount;
            float time = 0f;

            while (Mathf.Abs(_healthFill.fillAmount - targetFill) > 0.001f)
            {
                time += Time.deltaTime * _lerpSpeed;
                _healthFill.fillAmount = Mathf.Lerp(startFill, targetFill, time);
                yield return null;
            }

            _healthFill.fillAmount = targetFill;
        }

        public void Show(bool show)
        {
         gameObject.SetActive(show);   
        }
    }
}
