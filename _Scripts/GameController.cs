using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IEnemies
{
    void AddMeToList();
    void DestroyMe();
}

public interface IBullets
{
    void AddMeToList();
    void DestroyMe();
}

public class GameController : MonoBehaviour
{
    public static GameController Instance { set; get; }

    public List<IEnemies> m_enemiesList = new List<IEnemies>();
    public List<IBullets> m_bulletsList = new List<IBullets>();

    public EnemySpawner m_enemySpawner;
    public PlayerController m_player;

    public int m_initialLives = 5;
    [HideInInspector] public int m_lives;
    [HideInInspector] public int m_score = 0;
    [HideInInspector] public int m_highScore;

    public Text m_livesText, m_scoreText, m_highScoreText;
    public Canvas m_loseCanvas;

    [HideInInspector] public bool m_isGameStopped = false;

    public bool m_hideMouse;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveManager.Instance.Load();

        if (m_hideMouse)
            Cursor.visible = false;

        m_lives = m_initialLives;
        m_highScore = SaveManager.Instance.m_saveState.m_highScore;

        m_loseCanvas.enabled = false;
        m_livesText.text = "Lives: " + m_lives.ToString();
        m_scoreText.text = "Score: " + m_score.ToString();
        m_highScoreText.text = "High Score: " + m_highScore.ToString();

    }

    public void EnemiesLeft() //comprueba si quedan enemigos, si no, llama al enemyspawner para que instancie más
    {
        if (m_enemiesList.Count <= 0)
        {
            m_enemySpawner.SpawnEnemies();
        }
    }

    public void UpdateScore()
    {
        m_scoreText.text = "Score: " + m_score.ToString();
    }

    public void UpdateLives()
    {
        m_livesText.text = "Lives: " + m_lives.ToString();

        if (m_lives <= 0)
        {
            EndGame();
        }
    }

    public void UpdateHighScore()
    {
        SaveManager.Instance.Load();
        m_highScore = SaveManager.Instance.m_saveState.m_highScore;
        m_highScoreText.text = "High Score: " + m_highScore.ToString();
    }

    public void EndGame()
    {
        m_loseCanvas.enabled = true;
        Cursor.visible = true;
        m_isGameStopped = true;

        if(m_score > SaveManager.Instance.m_saveState.m_highScore)
        {
            m_highScore = m_score;
            SaveManager.Instance.Save();
        }

        //canvas retry, menu
    }

    public void NavigateToMenu()
    {

    }

    public void PlayerEnemyCollision(EnemyMovement enemy)
    {
        m_lives--;
        UpdateLives();
        m_enemiesList.Remove(enemy);
        EnemiesLeft();

    }

    public void BulletEnemyCollision(EnemyMovement enemy, BulletScript bullet)
    {
        m_score += 50;
        UpdateScore();
        m_bulletsList.Remove(bullet);
        m_enemiesList.Remove(enemy);
        EnemiesLeft();
    }

    public void Restart()
    {
        m_loseCanvas.enabled = false;
        m_enemySpawner.m_numberEnemies = m_enemySpawner.m_startNumberEnemies;
        m_score = 0;
        m_lives = m_initialLives;

        UpdateLives();
        UpdateScore();
        UpdateHighScore();
        Cursor.visible = m_hideMouse;
        m_isGameStopped = false;

        foreach (IEnemies e in m_enemiesList)
        {
            e.DestroyMe();
        }

        m_enemiesList.Clear();

        foreach (IBullets b in m_bulletsList)
        {
            b.DestroyMe();
        }

        m_bulletsList.Clear();

        EnemiesLeft();
    }
}
