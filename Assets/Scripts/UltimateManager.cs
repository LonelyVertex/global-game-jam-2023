using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UltimateManager : MonoBehaviour
{
    [SerializeField] float ultimateCooldown;

    [SerializeField] Button hornetsButton;
    [SerializeField] Button rainButton;

    [Space] 
    [SerializeField] GameObject hornetsEffect;
    [SerializeField] GameObject rainEffect;

    [Space] 
    [SerializeField] int hornetsDamage;
    [SerializeField] int hornetsWaves;
    [SerializeField] int rainHealing;
    [SerializeField] int rainWaves;

    bool IsUsable => Time.time - _lastUsed >= ultimateCooldown;

    float _lastUsed;

    void Start()
    {
        _lastUsed = -ultimateCooldown;

        hornetsButton.onClick.AddListener(() =>
        {
            if (IsUsable)
            {
                UseHornets();
            }
        });

        rainButton.onClick.AddListener(() =>
        {
            if (IsUsable)
            {
                UseRain();
            }
        });
    }

    void Update()
    {
        hornetsButton.interactable = IsUsable;
        rainButton.interactable = IsUsable;

        if (!IsUsable) return;

        if (Input.GetKeyUp(KeyCode.E))
        {
            UseHornets();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            UseRain();
        }
    }


    public void UseHornets()
    {
        StartCoroutine(Hornets());

        _lastUsed = Time.time;
    }

    IEnumerator Hornets()
    {
        hornetsEffect.SetActive(true);
        
        for (var i = 0; i < hornetsWaves; i++)
        {
            var enemies = FindObjectsOfType<EnemyBehaviour>();
            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(hornetsDamage);
            }

            yield return new WaitForSeconds(.5f);
        }
        
        hornetsEffect.SetActive(false);
    }

    public void UseRain()
    {
        StartCoroutine(Rain());
        _lastUsed = Time.time;
    }
    
    IEnumerator Rain()
    {
        rainEffect.SetActive(true);
        
        for (var i = 0; i < rainWaves; i++)
        {
            var trees = FindObjectsOfType<TreeBehaviour>();
            foreach (var tree in trees)
            {
                tree.TakeHealing(rainHealing);
            }

            yield return new WaitForSeconds(.5f);
        }
        
        rainEffect.SetActive(false);
    }
}
