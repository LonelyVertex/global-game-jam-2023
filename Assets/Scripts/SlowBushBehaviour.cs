using UnityEngine;

public class SlowBushBehaviour : MonoBehaviour
{
    [SerializeField] int health;

    int _currentHealth;

    void Start()
    {
        _currentHealth = health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy)
        {
            enemy.AddSlowModifier();
            ReduceHealth();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy)
        {
            enemy.RemoveSlowModifier();
        }
    }

    void ReduceHealth()
    {
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}