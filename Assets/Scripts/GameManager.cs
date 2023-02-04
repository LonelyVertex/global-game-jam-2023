using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] float stageTime;
    [SerializeField] float baseGrowingSpeed;

    [Header("UI References")]
    [SerializeField] GameObject upgradePanel;

    public event Action<int> OnStageChange; 

    public static GameManager Instance { get; private set; }

    public float StageProgress => (Time.time - _startTime) / stageTime;
    public float GrowingSpeed => baseGrowingSpeed;
    
    float _startTime;
    int _currentStage = 1;
    
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