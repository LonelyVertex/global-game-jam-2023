using UnityEngine;
using UnityEngine.UI;

public class PlantingActionButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject prefab;
    [SerializeField] int cost;
    [SerializeField] float plantRadius;

    bool _canPlant;
    
    void Start()
    {
        button.onClick.AddListener(() => { PlantingManager.Instance.StartPlanting(prefab, cost, plantRadius); });
    }

    void Update()
    {
        button.interactable = !PlantingManager.Instance.IsPlanting &&  cost <= PlantingManager.Instance.LifeForce;
    }
}