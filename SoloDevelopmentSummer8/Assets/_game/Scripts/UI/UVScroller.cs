using UnityEngine;
using UnityEngine.UI;

namespace _game.Scripts.UI
{
    public class UVScroller : MonoBehaviour
    {
        [Header("Scroll")]
        [SerializeField] private Vector2 _scrollSpeed = new Vector2(0f, -0.1f);

        [Header("Wiggle")] 
        [SerializeField] private bool _wiggle = true;
        [SerializeField] private Vector2 _wiggleSpeed = new Vector2(0.05f, 0f);
        [SerializeField] private Vector2 _wiggleMagnitude = new Vector2(0.05f, 0.05f);
        [SerializeField] private RawImage _rawImage;

        private Vector2 _currentWiggle;
        private float _time;
        
        

        private void Start()
        {
            if (!_wiggle) _currentWiggle = Vector2.zero;
        }

        public void SetScrollSpeed(float speed)
        {
            _scrollSpeed = new Vector2(speed, _scrollSpeed.y);
        }
        private void Update()
        {
            CalculateWiggle();
            
            var effectiveSpeed = _scrollSpeed + _currentWiggle;
            var offset = Time.time * effectiveSpeed;

            _rawImage.uvRect = new Rect(offset, Vector2.one);
        }

        private void CalculateWiggle()
        {
            if (!_wiggle) return;

            _time += Time.deltaTime;
            
            _currentWiggle = new Vector2(
                _wiggleSpeed.x * (Mathf.Sin(_time) * _wiggleMagnitude.x),
                _wiggleSpeed.y * (Mathf.Sin(_time) * _wiggleMagnitude.y)
            );
        }

        private void OnValidate()
        {
            if (_rawImage == null)
                _rawImage = GetComponent<RawImage>();
        }
    }
}