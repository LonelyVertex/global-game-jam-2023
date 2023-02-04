using UnityEngine;

public class StageResizer : MonoBehaviour
{
    float _desiredScale;

    void Start()
    {
        _desiredScale = 1;

        GameManager.Instance.OnStageChange += OnStageChange;
    }

    void OnStageChange(int stage)
    {
        _desiredScale = stage;
    }

    protected virtual void Update()
    {
        if (IsResized()) return;

        var scale = Mathf.Lerp(transform.localScale.x, _desiredScale,
            GameManager.Instance.ResizeSpeed * Time.deltaTime);
        transform.localScale = new Vector3(scale, scale, 1);
    }

    protected bool IsResized()
    {
        return Mathf.Approximately(transform.localScale.x, _desiredScale);
    }
}