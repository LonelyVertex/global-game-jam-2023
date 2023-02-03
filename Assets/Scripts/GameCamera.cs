using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float _baseSize;
    [SerializeField] float _stageSize;
    [SerializeField] float _resizeSpeed;

    float _desiredSize;

    void Start()
    {
        _desiredSize = _baseSize;

        GameManager.Instance.OnStageChange += OnStageChange;
    }

    void OnStageChange(int stage)
    {
        _desiredSize = _baseSize + (stage - 1) * _stageSize;
    }

    void Update()
    {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _desiredSize, _resizeSpeed * Time.deltaTime);
    }
}