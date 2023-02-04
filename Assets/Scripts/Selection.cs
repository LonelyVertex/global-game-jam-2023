using System;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color defaultColor;
    [SerializeField] Color disabledColor;
    [SerializeField] LayerMask treeLayer;

    public Action<Vector3> OnSelect;

    float _radius;
    bool _canPlant;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    void Start()
    {
        spriteRenderer.color = defaultColor;
    }

    public void SetRadius(float radius)
    {
        _radius = radius;
    }

    void Update()
    {
        if (PlantingManager.Instance.IsPlanting) return;

        var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        
        CheckCanPlant();

        if (_canPlant && Input.GetMouseButtonUp(0))
        {
            OnSelect?.Invoke(transform.position);
        }
    }

    void CheckCanPlant()
    {
        if (_radius > 0 && Physics2D.OverlapCircle(transform.position, _radius, treeLayer))
        {
            spriteRenderer.color = disabledColor;
            _canPlant = false;
        }
        else
        {
            spriteRenderer.color = defaultColor;
            _canPlant = true;
        }
    }
}