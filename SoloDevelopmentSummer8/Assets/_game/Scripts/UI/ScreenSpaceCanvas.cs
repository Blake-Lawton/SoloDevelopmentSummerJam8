using System;
using UnityEngine;

namespace _game.Scripts.UI
{
    public class ScreenSpaceCanvas : MonoBehaviour
    {
        public static ScreenSpaceCanvas Instance;
        private Canvas _canvas;
        
        public Canvas Canvas => _canvas;
        private void Awake()
        {
            Instance = this;
            _canvas = GetComponent<Canvas>();
        }
    }
}
