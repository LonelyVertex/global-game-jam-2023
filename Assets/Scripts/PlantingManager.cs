using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    [SerializeField] Selection _selection;
    [SerializeField] GrowingRoot _growingRoot;

    [Header("Prefabs")] 
    [SerializeField] GameObject _attackPrefab;
    [SerializeField] GameObject _tree1Prefab;

    GameObject _currentPrefab;
    Vector3 _targetPosition;
    
    void Start()
    {
        _selection.OnSelect += OnSelect;
        _growingRoot.OnReachDestination += OnReachDestination;
    }

    public void UseAttack()
    {
        _currentPrefab = _attackPrefab;
        _selection.gameObject.SetActive(true);
    }

    public void UseTree1()
    {
        _currentPrefab = _tree1Prefab;
        _selection.gameObject.SetActive(true);
    }

    void OnSelect(Vector3 position)
    {
        _targetPosition = position;
        _selection.gameObject.SetActive(false);
        _growingRoot.SetTarget(position);
    }

    void OnReachDestination()
    {
        Instantiate(_currentPrefab, _targetPosition, Quaternion.identity); 
    }
}
