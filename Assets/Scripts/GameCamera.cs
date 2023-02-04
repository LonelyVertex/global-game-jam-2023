using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] new Camera camera;
    [SerializeField] float baseSize;
    [SerializeField] float stageSize;

    float _currentSize;
    float _desiredSize;

    void Start()
    {
        _currentSize = baseSize;
        _desiredSize = baseSize;

        GameManager.Instance.OnBeforeStageChange += OnBeforeStageChange;
    }

    void OnBeforeStageChange(int stage)
    {
        _currentSize = _desiredSize;
        _desiredSize = baseSize + (stage - 1) * stageSize;
    }

    void Update()
    {
        if (GameManager.Instance.IsTransitioning)
        {
            camera.orthographicSize = Mathf.Lerp(_currentSize, _desiredSize,
                GameManager.Instance.TransitionProgress);
        }
    }
}