using System;
using _game.Scripts.Controllers.Enemy;
using _game.Scripts.Controllers.Player;
using _game.Scripts.Data;
using _game.Scripts.Global;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _game.Scripts.Managers
{
    public class GameStateManager : MonoBehaviour
    {
       [SerializeField] private GameState _gameState;
       [SerializeField] private SpawnManager _spawnManager;
       [SerializeField] private ChaosController _chaos;
       [SerializeField] private GameObject _upgradesMenu;
       public GameState GameState => _gameState;

       [Header("Round")] 
       [SerializeField]private TMP_Text _roundText;
       private int _round = 1;
       [SerializeField]private float _roundScaler = 1;
       
       public float RoundScaler => _roundScaler;

        public static GameStateManager Instance { get; private set; }
       private void Awake()
       {
           Instance = this;
           GlobalEvents.OnEndRound += EndRound;
           Application.targetFrameRate = 144;
       }

       
       public void Update()
       {
           switch (_gameState)
           {
               case GameState.InRound:
                   if(_chaos.ChaosMode)
                       _spawnManager.SetSpawnTime(.1f);
                   else
                   {
                       _spawnManager.SetSpawnTime(1 - _chaos.CurrentChaos / _chaos.MaxChaos);
                   }
                   _spawnManager.SpawnEnemies(_roundScaler);
                   break;
               case GameState.Upgrades:
                   break;
           }
       }

       [Button]
       public void NextRound()
       {
           _roundScaler += .5f;
           _round++;
           _gameState = GameState.InRound;
           _roundText.text = "Round " + _round;
           _chaos.CanGainChaos = true;
       }
       
       private void EndRound()
       {
           _gameState = GameState.Upgrades;
           var enemies = FindObjectsByType<EnemyDespawn>(FindObjectsSortMode.None);

           foreach (var enemy in enemies)
           {
               enemy.GetComponent<HealthController>().TakeMaxDamage();
           }
           _upgradesMenu.SetActive(true);
       }
       
       public void StartGame()
       {
           _gameState = GameState.InRound;
           _roundScaler = 1f;
           _round = 1;
           _roundText.text = "Round " + _round;
       }

       public void EndGame()
       {
           
           _gameState = GameState.EndGame;
           var enemies = FindObjectsByType<EnemyDespawn>(FindObjectsSortMode.None);

           foreach (var enemy in enemies)
           {
               enemy.GetComponent<HealthController>().TakeMaxDamage();
           }
       }
    }
    

}
