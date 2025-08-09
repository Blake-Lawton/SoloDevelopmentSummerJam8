using System;
using UnityEngine;

namespace _game.Scripts.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyStats _stats;
        private CharacterController _cc;

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();
        }

        public virtual void Movement()
        {
            Vector3 direction = PlayerBrain.Instance.transform.position - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.001f) // only rotate if moving
            {
                // Rotate smoothly towards the player
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }

            _cc.Move(direction.normalized * _stats.Speed * Time.deltaTime);
        }

        private void Update()
        {
            Movement();
        }
    }
}
