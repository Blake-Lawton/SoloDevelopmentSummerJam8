using System;
using TMPro;
using UnityEngine;

namespace _game.Scripts.Debug
{
   public class DEBUG_BUG_MAN : MonoBehaviour
   {
      [SerializeField] private TMP_Text fpsText;

      private int _frameCount;
      private float _elapsedTime;
      private float _fps;

      private void Update()
      {
         _frameCount++;
         _elapsedTime += Time.unscaledDeltaTime;  // Use unscaledDeltaTime so it's unaffected by Time.timeScale

         if (_elapsedTime >= 1f)  // Update FPS every 1 second
         {
            _fps = _frameCount / _elapsedTime;
            if (fpsText != null)
            {
               fpsText.text = $"FPS: {Mathf.RoundToInt(_fps)}";
            }
            _frameCount = 0;
            _elapsedTime = 0f;
         }
      }
   }
}
