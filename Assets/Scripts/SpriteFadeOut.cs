using UnityEngine;

public class SpriteFadeOut : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AnimationCurve fadeAnimation;
    [SerializeField] float duration;

    float _startTime;
    float _startAlpha;

    void Start()
    {
        _startTime = Time.time;
        _startAlpha = spriteRenderer.color.a;
        SetAlpha(0);
    }

    void Update()
    {
        var t = (Time.time - _startTime) / duration;

        if (t > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            SetAlpha(fadeAnimation.Evaluate(t) * _startAlpha);
        }
    }

    void SetAlpha(float a)
    {
        var spriteRendererColor = spriteRenderer.color;
        spriteRendererColor.a = a;
        spriteRenderer.color = spriteRendererColor;
    }
}