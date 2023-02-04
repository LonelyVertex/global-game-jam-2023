using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] CircleCollider2D circleCollider2D;

    public float HealthPctg => (float)_currentHealth / maxHealth;
    public float Radius => circleCollider2D.radius * transform.localScale.x;

    int _currentHealth;

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeHealing(int amount)
    {
        _currentHealth = Mathf.Min(maxHealth, _currentHealth + amount);
    }
    
    void Start()
    {
        _currentHealth = maxHealth;
    }
}