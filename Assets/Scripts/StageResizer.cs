using UnityEngine;

public class StageResizer : MonoBehaviour
{
    float _currentScale;
    float _desiredScale;

    void Start()
    {
        _currentScale = 1;
        _desiredScale = 1;

        GameManager.Instance.OnBeforeStageChange += OnBeforeStageChange;
    }

    void OnBeforeStageChange(int stage)
    {
        _currentScale = _desiredScale;
        _desiredScale = stage;
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.IsTransitioning)
        {
            var scale = Mathf.Lerp(_currentScale, _desiredScale, GameManager.Instance.TransitionProgress);
            transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}