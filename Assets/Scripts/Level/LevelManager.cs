using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;

    public int TotalLives { get; private set; }
    public int CurrentWave { get; private set; }

    private void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;
    }

    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        // Outras ações que você queira realizar ao fim do jogo
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        // Lógica de conclusão da onda
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        // Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
    }
}
