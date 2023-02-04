using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] new Camera camera;
    [SerializeField] float baseSize;
    [SerializeField] float stageSize;

    float _desiredSize;

    void Start()
    {
        _desiredSize = baseSize;

        GameManager.Instance.OnStageChange += OnStageChange;
    }

    void OnStageChange(int stage)
    {
        _desiredSize = baseSize + (stage - 1) * stageSize;
    }

    void Update()
    {
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, _desiredSize, GameManager.Instance.ResizeSpeed * Time.deltaTime);
    }
}