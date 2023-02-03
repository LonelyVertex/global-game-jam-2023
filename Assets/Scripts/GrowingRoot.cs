using System;
using UnityEngine;

public class GrowingRoot : MonoBehaviour
{
    [SerializeField] LineRenderer _lineRenderer;

    public Action OnReachDestination;
    
    Vector3 _startPosition;
    Vector3 _targetPosition;
    float _distance;

    bool _isGrowing;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    public void SetTarget(Vector3 target)
    {
        _targetPosition = target;
        _lineRenderer.SetPosition(1, _startPosition);
        _distance = Vector3.Distance(_startPosition, _targetPosition);
        _isGrowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGrowing) return;


        var currentPosition = _lineRenderer.GetPosition(1);

        if (Vector3.Distance(_startPosition, currentPosition) >= _distance)
        {
            OnReachDestination?.Invoke();
            _isGrowing = false;
            return;
        }

        var direction = _targetPosition - _startPosition;
        var target = _lineRenderer.GetPosition(1) + direction * GameManager.Instance.GrowingSpeed * Time.deltaTime;
        _lineRenderer.SetPosition(1, target);
    }
}