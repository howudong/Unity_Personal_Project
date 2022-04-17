using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }
    private static GameManager _instance;

    public float player_Damage { get; private set; }
    public float player_RPM { get; private set; }
    public float player_Speed { get; private set; }
    public int level { get;private set;}
    public int wave;

    [SerializeField] EnemySpawner enemySpawner;

    private void Awake()
    {
        if (instance != this) Destroy(gameObject);
    }
    public bool isGameOver;
    public int score;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            isGameOver = false;
            score = 0;
            level = 1;
            wave = 0;

            player_Damage = 5f;
            player_RPM = 0.2f;
            player_Speed = 4f;
            UIManager.instance.UpdateStatText(player_Damage, player_RPM, player_Speed);
        }
    }
    public void AddScore(int newScore)
    {
        score += newScore;
        UIManager.instance.UpdateScoreText(score);
    }
    public void EndGame()
    {
        isGameOver = true;
        UIManager.instance.ActiveGameOverPanel(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UIManager.instance.ActiveGameOverPanel(false);
    }
    public void MoveScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void LevelUp()
    {
        level++;
        UIManager.instance.SetActiveStatUpBoard(true);
        Time.timeScale = 0;
    }
    public void StatUp(int index)
    {
        switch (index)
        {
            case 1: player_Damage += 2;
                break;
            case 2: player_RPM -= 0.02f;
                break;
            case 3: player_Speed += 0.2f;
                break;
        }
        UIManager.instance.UpdateStatText(player_Damage, player_RPM, player_Speed);
        UIManager.instance.SetActiveStatUpBoard(false);
        Time.timeScale = 1;
    }
}
