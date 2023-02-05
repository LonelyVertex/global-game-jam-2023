using System.Linq;
using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    public const float LifeForceEvery = 3;

    [SerializeField] Selection selection;
    [SerializeField] GameObject growingRootPrefab;

    public static PlantingManager Instance { get; private set; }

    public int LifeForce => _lifeForce;
    public bool CanPlant => !_isDisabled && !_isPlanting;

    int _lifeForce = 100;

    bool _isSelecting;
    GameObject _currentPrefab;
    int _currentCost;

    bool _isPlanting;
    Vector3 _targetPosition;
    GrowingRoot _currentGrowingRoot;
    bool _currentGrowingRootRemains;

    bool _isDisabled;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        selection.OnSelect += OnSelect;

        GameManager.Instance.OnBeforeStageChange += OnBeforeStageChange;
        GameManager.Instance.OnAfterStageChange += OnAfterStageChange;
    }

    void OnBeforeStageChange(int stage)
    {
        CancelSelection();
        _isDisabled = true;
    }

    void OnAfterStageChange()
    {
        _isDisabled = false;
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

    public void StartPlanting(GameObject prefab, int cost, float radius, bool remains, int cursor)
    {
        _isSelecting = true;
        _currentPrefab = prefab;
        _currentCost = cost;
        _currentGrowingRootRemains = remains;
        selection.gameObject.SetActive(true);
        selection.SetCursorRenderer(cursor);
        selection.SetRadius(radius);
    }

    void OnSelect(Vector3 position)
    {
        _isSelecting = false;
        _isPlanting = true;
        _targetPosition = position;
        _lifeForce -= _currentCost;

        var rootGO = Instantiate(growingRootPrefab, GetGrowingStartPosition(position), Quaternion.identity);
        _currentGrowingRoot = rootGO.GetComponent<GrowingRoot>();
        _currentGrowingRoot.OnReachDestination += OnReachDestination;
        _currentGrowingRoot.SetTarget(position);
    }

    Vector3 GetGrowingStartPosition(Vector3 targetPosition)
    {
        var closest = FindObjectsOfType<LifeForceGenerator>()
            .OrderBy(tree =>
            {
                var treeRadius = tree.GetComponent<TreeBehaviour>().Radius;
                return Vector3.Distance(tree.transform.position, targetPosition) - treeRadius;
            })
            .FirstOrDefault();

        return closest != null ? closest.transform.position : Vector3.zero;
    }

    void OnReachDestination()
    {
        _isPlanting = false;
        selection.gameObject.SetActive(false);

        var plantedGO = Instantiate(_currentPrefab, _targetPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        plantedGO.transform.localScale =
            new Vector3(GameManager.Instance.CurrentStage, GameManager.Instance.CurrentStage, 1);

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