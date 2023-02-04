using UnityEngine;

public class SlowBushBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy)
        {
            enemy.AddSlowModifier();
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
}