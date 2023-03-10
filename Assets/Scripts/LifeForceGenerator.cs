using UnityEngine;

public class LifeForceGenerator : MonoBehaviour
{
    [SerializeField] int baseIncrement;

    public int BaseIncrement => baseIncrement;
    
    void Start()
    {
        InvokeRepeating(nameof(AddLifeForce), PlantingManager.LifeForceEvery, PlantingManager.LifeForceEvery);
    }

    void AddLifeForce()
    {
        var increment = Mathf.RoundToInt(baseIncrement * GameManager.Instance.LifeForceGenerateModifier);
        PlantingManager.Instance.AddLifeForce(increment);
    }

    public void AddIncrement(int amount)
    {
        baseIncrement += amount;
    }
}