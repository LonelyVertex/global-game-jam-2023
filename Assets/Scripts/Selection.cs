using System;
using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] spriteRenderers;
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
        SetColor(defaultColor);
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
            SetColor(disabledColor);
            _canPlant = false;
        }
        else
        {
            SetColor(defaultColor);
            _canPlant = true;
        }
    }

    void SetColor(Color color)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = color;
        }
    }

    public void SetCursorRenderer(int selected)
    {
        for (var i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].gameObject.SetActive(i == selected);
        }
    }
}