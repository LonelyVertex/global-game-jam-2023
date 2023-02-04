using System.Linq;
using UnityEngine;

public class PoisonTreeBehaviour : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float radius;
    [SerializeField] float damageCooldown;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Start()
    {
        InvokeRepeating(nameof(DamageEnemies), 0, damageCooldown);
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
}
