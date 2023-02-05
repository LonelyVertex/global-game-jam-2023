using UnityEngine;

public class SpriteFadeOut : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float duration;

    float _startTime;
    float _startAlpha;

    void Start()
    {
        _startTime = Time.time;
        _startAlpha = spriteRenderer.color.a;
    }

    void Update()
    {
        var t = (Time.time - _startTime) / duration;

        if (t <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            var spriteRendererColor = spriteRenderer.color;
            spriteRendererColor.a = Mathf.Lerp(_startAlpha, 0, t);
            spriteRenderer.color = spriteRendererColor;
        }
    }
}