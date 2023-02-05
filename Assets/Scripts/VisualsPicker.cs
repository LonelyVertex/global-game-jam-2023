using UnityEngine;

public class VisualsPicker : MonoBehaviour
{
    [SerializeField] private GameObject[] _visuals;
    
    void Awake()
    {
        foreach (var go in _visuals)
        {
            go.SetActive(false);
        }
        
        _visuals[Random.Range(0, _visuals.Length)].SetActive(true);
    }
}
