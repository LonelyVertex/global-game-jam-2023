using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] float stageTime;
    [SerializeField] float transitionTime;
    [SerializeField] float baseGrowingSpeed;
    [SerializeField] float baseSlow;
    [SerializeField] float enemyStageSpeedIncrease;

    [Header("Game References")]
    [SerializeField] TreeBehaviour motherTree;

    [Header("UI References")] 
    [SerializeField] Slider motherTreeHealthSlider;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameOverStageText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_InputField nicknameInput;
    [SerializeField] TMP_Text stageText;


    public event Action<int> OnBeforeStageChange;
    public event Action OnAfterStageChange;
    public event Action OnGameOver;

    public static GameManager Instance { get; private set; }

    public float StageProgress
    {
        get
        {
            var timeElapsed = Time.time - _startTime;
            var stagesDone = IsTransitioning ? CurrentStage - 1 : CurrentStage;
            var pastTransitions = transitionTime * (Mathf.Max(0, stagesDone - 1));
            var currentTransition = IsTransitioning ? (Time.time - _transitionStartTime) : 0;

            return (timeElapsed - pastTransitions - currentTransition) / stageTime;
        }
    }

    public bool IsGameOver => _isGameOver;

    public float GrowingSpeed => baseGrowingSpeed;
    public float LifeForceGenerateModifier => _lifeForceGenerateModifier;
    public int CurrentStage => _currentStage;
    public float SlowModifier => baseSlow;
    public bool IsTransitioning => _isTransitioning;
    public float TransitionProgress => (Time.time - _transitionStartTime) / transitionTime;
    public float EnemySpeedModifier => 1 + (CurrentStage - 1) * enemyStageSpeedIncrease;

    float _startTime;
    int _score;
    int _currentStage = 1;
    float _lifeForceGenerateModifier = 1;

    bool _isTransitioning;
    float _transitionStartTime;
    bool _isGameOver;
    private SaveDataController _saveDataController = new ();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        if (_isGameOver)
        {
            return;
        }

        if (StageProgress > _currentStage)
        {
            _currentStage++;
            DestroyEnemies();
            // upgradePanel.SetActive(true);

            _isTransitioning = true;
            _transitionStartTime = Time.time;

            Invoke(nameof(StartStage), transitionTime);
            OnBeforeStageChange?.Invoke(_currentStage);
        }

        motherTreeHealthSlider.value = motherTree.HealthPctg;

        if (motherTree.HealthPctg <= 0)
        {
            GameOver();
        }
    }

    void StartStage()
    {
        _isTransitioning = false;
        stageText.text = $"{CurrentStage}";
        OnAfterStageChange?.Invoke();
    }

    void DestroyEnemies()
    {
        var enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    void GameOver()
    {
        _isGameOver = true;
        
        DestroyEnemies();
        
        OnGameOver?.Invoke();

        _score = Mathf.RoundToInt((Time.time - _startTime) * 100);
        
        gameOverPanel.SetActive(true);
        gameOverStageText.text = $"You made it to stage {_currentStage}!";
        scoreText.text = $"Score {_score}";
    }

    public void Restart()
    {
        SaveScore();
        
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SaveScore();
        
        SceneManager.LoadScene(0);
    }

    private void SaveScore()
    {
        var nickname = nicknameInput.text;

        if (!string.IsNullOrEmpty(nickname) && _score > 0)
        {
            _saveDataController.SaveHighScore(nickname, _score);
        }
    }
}