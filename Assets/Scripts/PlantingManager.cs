using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    public const float LifeForceEvery = 3; 
    
    [SerializeField] Selection selection;
    [SerializeField] GrowingRoot growingRoot;

    [Header("Prefabs")] [SerializeField] GameObject attackPrefab;
    [SerializeField] GameObject tree1Prefab;

    public static PlantingManager Instance { get; private set; }

    public int LifeForce => _lifeForce;

    GameObject _currentPrefab;
    int _currentCost;
    Vector3 _targetPosition;

    int _lifeForce;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        selection.OnSelect += OnSelect;
        growingRoot.OnReachDestination += OnReachDestination;
    }

    public void AddLifeForce(int amount)
    {
        _lifeForce += amount;
    }

    public void StartPlanting(GameObject prefab, int cost)
    {
        _currentPrefab = prefab;
        _currentCost = cost;
        selection.gameObject.SetActive(true);
    }

    // public void UseAttack()
    // {
    //     _currentPrefab = attackPrefab;
    //     selection.gameObject.SetActive(true);
    // }
    //
    // public void UseTree1()
    // {
    //     _currentPrefab = tree1Prefab;
    //     selection.gameObject.SetActive(true);
    // }

    void OnSelect(Vector3 position)
    {
        _targetPosition = position;
        _lifeForce -= _currentCost;
        selection.gameObject.SetActive(false);
        growingRoot.SetTarget(position);
    }

    void OnReachDestination()
    {
        Instantiate(_currentPrefab, _targetPosition, Quaternion.identity);
    }
}