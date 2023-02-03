using System;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public Action<Vector3> OnSelect;
    
    void Update()
    {
        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

        if (Input.GetMouseButtonUp(0))
        {
            OnSelect?.Invoke(transform.position);
        }
    }
}
