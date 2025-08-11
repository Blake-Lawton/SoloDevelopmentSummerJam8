using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.Controllers.Enemy
{
    public class EnemyMovementController : MonoBehaviour , IController
    {
        [SerializeField] private EnemyStats _stats;
        private CharacterController _cc;
        private float _speed;
        public bool IsMoving {get;  set;}
        private bool IsRotating { get; set; }
        private void Awake()
        {
            _speed = _stats.Speed;
            _cc = GetComponent<CharacterController>();
            IsMoving = true;
            IsRotating = true;
        }

        public virtual void Movement()
        {
            Vector3 direction = PlayerBrain.Instance.transform.position - transform.position;
            direction.y = 0;
            
            if (IsRotating)
            {
                if (direction.sqrMagnitude > 0.001f) 
                {
                    // Rotate smoothly towards the player
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
                }
            }
            
            if(IsMoving && _cc.enabled)
                _cc.Move(direction.normalized * _stats.Speed * Time.deltaTime);

           
        }

        public void Handle()
        {
            Movement();
        }

        public void Death()
        {
            IsMoving = false;
            IsRotating = false;
            _cc.enabled = false;
        }

        public void IncreaseSpeed(float roundScaler)
        {
            _speed = _stats.Speed * roundScaler;
        }

       
    }
}
