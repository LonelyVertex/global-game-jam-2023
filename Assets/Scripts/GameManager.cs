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


    public event Action<int> OnBeforeStageChange;
    public event Action OnAfterStageChange;

    public static GameManager Instance { get; private set; }

    public float StageProgress => (Time.time - _startTime) / stageTime;
    public float GrowingSpeed => baseGrowingSpeed;
    public float LifeForceGenerateModifier => _lifeForceGenerateModifier;
    public int CurrentStage => _currentStage;
    public float SlowModifier => baseSlow;
    public bool IsTransitioning => _isTransitioning;
    public float TransitionProgress => (Time.time - _transitionStartTime) / transitionTime;
    public float EnemySpeedModifier => 1 + (CurrentStage - 1) * enemyStageSpeedIncrease;

    float _startTime;
    int _currentStage = 1;
    float _lifeForceGenerateModifier = 1;
    
    bool _isTransitioning;
    float _transitionStartTime;

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
        OnAfterStageChange?.Invoke();
    }

    // public void OnUpgrade1Selected()
    // {
    //     // ...
    //     OnAfterUpgrade();
    // }

    // void OnAfterUpgrade()
    // {
    //     Time.timeScale = 1;
    //     upgradePanel.SetActive(false);
    //     OnStageChange?.Invoke(_currentStage);
    // }

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
        gameOverPanel.SetActive(true);
        gameOverStageText.text = $"You made it to stage {_currentStage}!";
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}