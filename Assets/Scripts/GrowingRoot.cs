using System;
using UnityEngine;

public class GrowingRoot : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] private GrowingRootAudioController _growingRootAudioController;
    
    public Action OnReachDestination;
    
    Vector3 _startPosition;
    Vector3 _targetPosition;
    float _distance;

    bool _isGrowing;
    

    public void SetTarget(Vector3 target)
    {
        _startPosition = transform.position;
        _targetPosition = target;
        lineRenderer.SetPosition(0, _startPosition);
        lineRenderer.SetPosition(1, _startPosition);
        _distance = Vector3.Distance(_startPosition, _targetPosition);
        _isGrowing = true;
        
        _growingRootAudioController.StartGrowing();
    }

    void Update()
    {
        if (!_isGrowing) return;


        var currentPosition = lineRenderer.GetPosition(1);

        if (Vector3.Distance(_startPosition, currentPosition) >= _distance)
        {
            OnReachDestination?.Invoke();
            _isGrowing = false;
            _growingRootAudioController.StopGrowing();
            return;
        }

        var direction = _targetPosition - _startPosition;
        var target = lineRenderer.GetPosition(1) + direction * (GameManager.Instance.GrowingSpeed * Time.deltaTime);
        lineRenderer.SetPosition(1, target);
    }
}