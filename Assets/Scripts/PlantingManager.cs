using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    [SerializeField] Selection selection;
    [SerializeField] GrowingRoot growingRoot;

    [Header("Prefabs")] 
    [SerializeField] GameObject attackPrefab;
    [SerializeField] GameObject tree1Prefab;

    GameObject _currentPrefab;
    Vector3 _targetPosition;
    
    void Start()
    {
        selection.OnSelect += OnSelect;
        growingRoot.OnReachDestination += OnReachDestination;
    }

    public void UseAttack()
    {
        _currentPrefab = attackPrefab;
        selection.gameObject.SetActive(true);
    }

    public void UseTree1()
    {
        _currentPrefab = tree1Prefab;
        selection.gameObject.SetActive(true);
    }

    void OnSelect(Vector3 position)
    {
        _targetPosition = position;
        selection.gameObject.SetActive(false);
        growingRoot.SetTarget(position);
    }

    void OnReachDestination()
    {
        Instantiate(_currentPrefab, _targetPosition, Quaternion.identity); 
    }
}
