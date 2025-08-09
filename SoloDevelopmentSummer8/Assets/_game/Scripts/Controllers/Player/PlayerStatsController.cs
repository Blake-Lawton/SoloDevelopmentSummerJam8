using _game.Scripts.Data;
using _game.Scripts.Data.PlayerData;
using _game.Scripts.Interfaces;
using UnityEngine;

namespace _game.Scripts.Controllers.Player
{
    public class PlayerStatsController : MonoBehaviour, IController
    {
        
        [SerializeField] private PlayerStatsData _data;
        private float _damageMulti = 1;
        
        public float DamageMulti => _damageMulti;
        public void Handle()
        {
          
        }
    }
}
