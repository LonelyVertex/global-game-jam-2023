using UnityEngine;

public class Attack : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(DestroyItself), 0.5f);
    }

    void DestroyItself()
    {
        Destroy(gameObject);
    }
}
