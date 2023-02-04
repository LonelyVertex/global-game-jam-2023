using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    public const float LifeForceEvery = 3;

    [SerializeField] Selection selection;
    [SerializeField] GrowingRoot growingRoot;
    [SerializeField] GameObject growingRootPrefab;

    public static PlantingManager Instance { get; private set; }

    public int LifeForce => _lifeForce;
    public bool IsPlanting => _isPlanting;

    int _lifeForce;

    bool _isSelecting;
    GameObject _currentPrefab;
    int _currentCost;

    bool _isPlanting;
    Vector3 _targetPosition;
    GrowingRoot _currentGrowingRoot;
    bool _currentGrowingRootRemains;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        selection.OnSelect += OnSelect;
        // growingRoot.OnReachDestination += OnReachDestination;
    }

    void Update()
    {
        if (_isSelecting && Input.GetKeyDown(KeyCode.Escape))
        {
            CancelSelection();
        }
    }

    public void AddLifeForce(int amount)
    {
        _lifeForce += amount;
    }

    public void StartPlanting(GameObject prefab, int cost, float radius, bool remains)
    {
        _isSelecting = true;
        _currentPrefab = prefab;
        _currentCost = cost;
        _currentGrowingRootRemains = remains;
        selection.gameObject.SetActive(true);
        selection.SetRadius(radius);
    }

    void OnSelect(Vector3 position)
    {
        _isSelecting = false;
        _isPlanting = true;
        _targetPosition = position;
        _lifeForce -= _currentCost;

        var rootGO = Instantiate(growingRootPrefab, Vector3.zero, Quaternion.identity);
        _currentGrowingRoot = rootGO.GetComponent<GrowingRoot>();
        _currentGrowingRoot.OnReachDestination += OnReachDestination;
        _currentGrowingRoot.SetTarget(position);
    }

    void OnReachDestination()
    {
        _isPlanting = false;
        selection.gameObject.SetActive(false);
        Instantiate(_currentPrefab, _targetPosition, Quaternion.identity);
        _currentGrowingRoot.OnReachDestination -= OnReachDestination;

        if (!_currentGrowingRootRemains)
        {
            Destroy(_currentGrowingRoot.gameObject);
        }
    }

    void CancelSelection()
    {
        selection.gameObject.SetActive(false);
    }
}