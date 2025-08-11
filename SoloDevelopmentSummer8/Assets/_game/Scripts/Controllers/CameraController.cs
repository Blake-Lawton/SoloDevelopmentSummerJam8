using _game.Scripts.Data;
using _game.Scripts.Managers;
using UnityEngine;

namespace _game.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;                 
        [SerializeField] private Vector3 _positionOffset = new Vector3(0f, 10f, -10f); 
        [SerializeField] private Vector3 _rotationOffset = new Vector3(45f, 0f, 0f);   
        [SerializeField] private float _followSpeed = 5f;           

        
        private void LateUpdate()
        {
            if(GameStateManager.Instance.GameState == GameState.Menu)
                return;
            if (!_target) return;

            Vector3 desiredPosition = _target.position + _positionOffset;
            Quaternion desiredRotation = Quaternion.Euler(_rotationOffset);

            transform.position = Vector3.Lerp(transform.position, desiredPosition, _followSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, _followSpeed * Time.deltaTime);
        }
    }
}
