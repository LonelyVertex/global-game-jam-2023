using UnityEngine;

public class Roots : MonoBehaviour
{
    [SerializeField] float minSize;
    [SerializeField] float stageSize;

    void Update()
    {
        var scale = Mathf.LerpUnclamped(minSize, stageSize, GameManager.Instance.StageProgress);
        SetScale(scale);
    }

    void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
