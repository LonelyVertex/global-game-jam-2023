using UnityEngine;
using UnityEngine.UI;

public class PlantingActionButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject prefab;
    [SerializeField] int cost;
    [SerializeField] float plantRadius;
    [SerializeField] bool rootRemains;
    [SerializeField] int cursor;
    [SerializeField] KeyCode keyCode;

    bool _canPlant;

    bool IsUsable => !PlantingManager.Instance.IsPlanting && cost <= PlantingManager.Instance.LifeForce;

    void Start()
    {
        button.onClick.AddListener(StartPlanting);
    }

    void Update()
    {
        button.interactable = IsUsable;

        if (IsUsable && Input.GetKeyUp(keyCode))
        {
            StartPlanting();
        }
    }

    void StartPlanting()
    {
        if (!IsUsable) return;
        PlantingManager.Instance.StartPlanting(prefab, cost, plantRadius, rootRemains, cursor);
    }
}