using UnityEngine;

public class StageResizer : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.OnBeforeStageChange += OnBeforeStageChange;
    }

    void OnBeforeStageChange(int stage)
    {
        transform.localScale = new Vector3(stage, stage, 1);
    }
}