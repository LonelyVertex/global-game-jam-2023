using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] float stageTime;
    [SerializeField] float baseGrowingSpeed;
    [SerializeField] float baseSlow;

    [Header("Game References")] 
    [SerializeField] TreeBehaviour motherTree;
    
    [Header("UI References")]
    [SerializeField] GameObject upgradePanel;
    [SerializeField] Slider motherTreeHealthSlider; 

    public event Action<int> OnStageChange; 

    public static GameManager Instance { get; private set; }

    public float StageProgress => (Time.time - _startTime) / stageTime;
    public float GrowingSpeed => baseGrowingSpeed;
    public float LifeForceGenerateModifier => _lifeForceGenerateModifier;
    public int CurrentState => _currentStage;
    public float SlowModifier => baseSlow;
    
    float _startTime;
    int _currentStage = 1;
    float _lifeForceGenerateModifier = 1;
    
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
            Time.timeScale = 0;
            upgradePanel.SetActive(true);
        }

        motherTreeHealthSlider.value = motherTree.HealthPctg;
    }

    public void OnUpgrade1Selected()
    {
        // ...
        OnAfterUpgrade();
    }

    void OnAfterUpgrade()
    {
        Time.timeScale = 1;
        upgradePanel.SetActive(false);
        OnStageChange?.Invoke(_currentStage);
    }
}