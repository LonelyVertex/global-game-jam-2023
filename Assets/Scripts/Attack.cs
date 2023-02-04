using System.Linq;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float radius;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Start()
    {
        Invoke(nameof(DamageEnemies), 0.1f);
        Invoke(nameof(DestroyItself), 0.5f);
    }

    void DamageEnemies()
    {
        var enemies = GameObject.FindObjectsOfType<EnemyBehaviour>()
            .Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= radius);

        foreach (var enemy in enemies)
        {
            enemy.TakeDamage(damage);
        }
    }

    void DestroyItself()
    {
        Destroy(gameObject);
    }
}
