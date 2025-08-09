using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.Controllers
{
    public class HealthController : MonoBehaviour, IController 
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;


       

        public void TakeDamage(float damage)
        {
           _currentHealth -= damage;
        }


        public void Handle()
        {
            
        }
    }
}
