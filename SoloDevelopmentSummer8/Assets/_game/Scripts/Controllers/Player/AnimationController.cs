using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.Controllers.Player
{
    public class AnimationController : MonoBehaviour, IController
    {
    
        [SerializeField] private Animator _animator;
        private MovementController _movement;


        private void Awake()
        {
            _movement = GetComponent<MovementController>();
        }
        
        public void Handle()
        {
            Animate();
        }
        
        public void Animate()
        {
            Vector3 moveDir = _movement.MovementDirection.normalized;

            float velocityZ = Vector3.Dot(moveDir, transform.forward); // forward/back
            float velocityX = Vector3.Dot(moveDir, transform.right);   // left/right

            _animator.SetFloat("Horizontal", velocityX, 0.1f, Time.deltaTime);
            _animator.SetFloat("Vertical", velocityZ, 0.1f, Time.deltaTime);
        }

    
    }
}
