using System;
using System.Collections.Generic;
using _game.Scripts.Controllers;
using _game.Scripts.Controllers.Enemy;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


namespace _game.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Spawn Locations")]
        [SerializeField] private float _minSpawnDistance;
        [SerializeField] private float _maxSpawnDistance; 
        // Define your map boundaries (assign these in inspector or elsewhere)
        [SerializeField] private Vector2 _mapMin = new Vector2(-30,-30); // e.g. (-50, -50)
        [SerializeField] private Vector2 _mapMax = new Vector2(30, 30);

        [Header("Spawn Info")]
        [SerializeField] private List<EnemyBrain> _enemyPrefabs;
        private int _currentWave;
       
        private float _spawnTimer;
        [SerializeField]private float _spawnTime = 1;
        public Vector3 GetSpawnPoint()
        {
            Vector3 playerPos = PlayerBrain.Instance.transform.position;
            float distance = Random.Range(_minSpawnDistance, _maxSpawnDistance);
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float offsetX = Mathf.Cos(angle) * distance;
            float offsetZ = Mathf.Sin(angle) * distance;
            Vector3 spawnPoint = new Vector3(playerPos.x + offsetX, playerPos.y, playerPos.z + offsetZ);
            float clampedX = Mathf.Clamp(spawnPoint.x, _mapMin.x, _mapMax.x);
            float clampedZ = Mathf.Clamp(spawnPoint.z, _mapMin.y, _mapMax.y);
            return new Vector3(clampedX, spawnPoint.y, clampedZ);
        }
        
        
        public void SpawnEnemies(float roundScaler)
        {
            _spawnTimer -= Time.deltaTime;
            _spawnTime = Mathf.Clamp(_spawnTime, 0.15f, 1);
            if (_spawnTimer <= 0)
            {
               
                EnemyBrain enemy = Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)],
                    GetSpawnPoint(), Quaternion.identity);

                enemy.SetUp(roundScaler);
                
                UnityEngine.Debug.Log("New Spawn Time " + _spawnTime);
               
                
                _spawnTimer = _spawnTime;
                
            }
        }

        public void SetSpawnTime(float spawnTime)
        {
            _spawnTime = spawnTime;
        }
    }
    
  
}
