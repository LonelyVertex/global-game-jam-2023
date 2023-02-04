using TMPro;
using UnityEngine;

public class LifeForceLabel : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    void Start()
    {
        text.text = "0";
    }

    void Update()
    {
        text.text = $"{PlantingManager.Instance.LifeForce}";
    }
}