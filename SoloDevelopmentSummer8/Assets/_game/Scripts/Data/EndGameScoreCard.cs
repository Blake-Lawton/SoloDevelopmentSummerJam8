using System;
using _game.Scripts.Data;
using _game.Scripts.Managers;
using TMPro;
using UnityEngine;

public class EndGameScoreCard : MonoBehaviour
{

    [SerializeField] private GameObject _scoreCard;
    [SerializeField] private TMP_Text _chaosCollectedText;
    [SerializeField] private TMP_Text _enemiesKilledText;
    [SerializeField] private TMP_Text _chaosHighScoreText;
    [SerializeField] private TMP_Text _enemiesKilledHighScoreText;
    public static EndGameScoreCard Instance;

    
    private int _chaosCollected;
    private int _enemiesKilled;
    
    private void Awake()
    {
        Instance = this;
    }


    public void EndGame()
    {
        _scoreCard.SetActive(true);
        _chaosCollectedText.text = "Chaos Collected: " + _chaosCollected;
        _enemiesKilledText.text = "Enemies Killed: " + _enemiesKilled;
        
        if(PlayerPrefs.GetInt("EnemiesKilled",0) <= _enemiesKilled)
            PlayerPrefs.SetInt("EnemiesKilled", _enemiesKilled);
        
        if(PlayerPrefs.GetInt("ChaosCollected", 0) <= _chaosCollected)
            PlayerPrefs.SetInt("ChaosCollected", _chaosCollected);
        
        _chaosHighScoreText.text = "Chaos Collected High Score: " + PlayerPrefs.GetInt("ChaosCollected", 0);
        _enemiesKilledHighScoreText.text = "Enemies Killed High Score: " + PlayerPrefs.GetInt("EnemiesKilled", 0);
    }

    public void ChaosCollect(int chaosCollected)
    {
        if(GameStateManager.Instance.GameState == GameState.EndGame)
            return;
        _chaosCollected += chaosCollected;
    }

    public void EnemyKill()
    {
        if(GameStateManager.Instance.GameState == GameState.EndGame)
            return;
        _enemiesKilled++;
    }

    public void ResetFresh()
    {
        _chaosCollected = 0;
        _enemiesKilled = 0;
    }
}
