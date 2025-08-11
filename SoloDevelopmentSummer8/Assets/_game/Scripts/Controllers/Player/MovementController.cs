using System;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts
{
    public class MovementController : MonoBehaviour, IController
    {
        [SerializeField] private CharacterController _cc;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _mouseLayerMask;
        private PlayerStatsController _stats;
        private Vector3 _movementDirection;
        public Vector3 MovementDirection => _movementDirection;


        private void Awake()
        {
            _stats = GetComponent<PlayerStatsController>();
        }

        public Vector3 RecordInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;
            _movementDirection = moveDir;
            return moveDir;
        }

        private void Move()
        {
            Vector3 moveDir = RecordInput();

            if (moveDir.magnitude >= 0.1f)
            {
                _cc.Move(moveDir * _stats.GetSpeed() * Time.deltaTime);
            }
        }

        private void RotateTowardsMouse()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _mouseLayerMask))
            {
                Vector3 lookDir = hit.point - transform.position;
                lookDir.y = 0f; // Keep rotation flat

                if (lookDir.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                }
            }
        }
        

        public void Handle()
        {
            Move();
            RotateTowardsMouse();
        }

        
    }
}
