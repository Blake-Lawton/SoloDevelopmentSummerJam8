using System;
using _game.Scripts.Controllers.Player;
using UnityEngine;

namespace _game.Scripts.Weapons.EnemyWeapons
{
    public class Projectile : MonoBehaviour
    {
        private int _damage;
        [SerializeField]private float _speed;
        [SerializeField] private float _lifeTime = 10;
        public void SetUp(int damage)
        {
            _damage = damage;
        }

        private void Update()
        {
            transform.position += transform.forward * Time.deltaTime * _speed;
            _lifeTime-= Time.deltaTime;
            if(_lifeTime <= 0)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
                other.GetComponent<HealthController>().TakeDamage(_damage);
            }
                
            
            
        }
    }
}
