using UnityEngine;

public class Roots : MonoBehaviour
{
    [SerializeField] float _minSize;
    [SerializeField] float _stageSize;

    void Update()
    {
        var scale = Mathf.LerpUnclamped(_minSize, _stageSize, GameManager.Instance.StageProgress);
        SetScale(scale);
    }

    void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
