using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float baseRadius;
    [SerializeField] float stageRadiusIncrement;
    [SerializeField] float spawnEnemyRate;

    float _currentRadius;
    float _lastEnemiesSpawned;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _currentRadius);
    }

    void Start()
    {
        _currentRadius = baseRadius;

        GameManager.Instance.OnStageChange += OnStageChanged;
    }

    void Update()
    {
        if (Time.time - _lastEnemiesSpawned >= spawnEnemyRate)
        {
            SpawnEnemies();
        }
    }

    void OnStageChanged(int currentState)
    {
        _currentRadius = baseRadius + (currentState - 1) * stageRadiusIncrement;
    }

    void SpawnEnemies()
    {
        for (var i = 0; i < GameManager.Instance.CurrentState; i++)
        {
            var position = Random.insideUnitCircle.normalized * _currentRadius;
            Instantiate(enemyPrefab, position, Quaternion.identity);
        }

        _lastEnemiesSpawned = Time.time;
    }
}