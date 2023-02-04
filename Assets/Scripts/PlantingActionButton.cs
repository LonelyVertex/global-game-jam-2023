using UnityEngine;
using UnityEngine.UI;

public class PlantingActionButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] int cost;
    [SerializeField] GameObject prefab;

    void Start()
    {
        button.onClick.AddListener(() => { PlantingManager.Instance.StartPlanting(prefab, cost); });
    }

    void Update()
    {
        button.interactable = cost <= PlantingManager.Instance.LifeForce;
    }
}