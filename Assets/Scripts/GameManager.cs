using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] float _stageTime;

    [Header("UI References")]
    [SerializeField] GameObject _upgradePanel;

    public event Action<int> OnStageChange; 

    public static GameManager Instance { get; private set; }

    public float StageProgress => (Time.time - _startTime) / _stageTime;
    public int CurrentStage => _currentStage;
    
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
            _upgradePanel.SetActive(true);
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
        _upgradePanel.SetActive(false);
        OnStageChange?.Invoke(_currentStage);
    }
}