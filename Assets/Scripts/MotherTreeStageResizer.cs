using System.Linq;
using UnityEngine;

public class MotherTreeStageResizer : MonoBehaviour
{
    [SerializeField] LifeForceGenerator lifeForceGenerator;
    [SerializeField] CircleCollider2D circleCollider2D;

    float _currentScale;
    float _desiredScale;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, circleCollider2D.radius * transform.localScale.x);
    }

    void Start()
    {
        _currentScale = 1;
        _desiredScale = 1;

        GameManager.Instance.OnBeforeStageChange += OnBeforeStageChange;
    }

    void OnBeforeStageChange(int stage)
    {
        _currentScale = _desiredScale;
        _desiredScale = stage;
    }

    void Update()
    {
        if (GameManager.Instance.IsTransitioning)
        {
            var scale = Mathf.Lerp(_currentScale, _desiredScale, GameManager.Instance.TransitionProgress);
            transform.localScale = new Vector3(scale, scale, 1);

            DevourTrees();
        }
    }

    void DevourTrees()
    {
        var trees = FindObjectsOfType<TreeBehaviour>()
            .Where(tree => Vector3.Distance(tree.transform.position, transform.position) <=
                           circleCollider2D.radius * transform.localScale.x);

        foreach (var tree in trees)
        {
            if (tree.gameObject != gameObject)
            {
                var treeLifeForceGenerator = tree.GetComponent<LifeForceGenerator>();
                if (treeLifeForceGenerator)
                {
                    lifeForceGenerator.AddIncrement(treeLifeForceGenerator.BaseIncrement);
                }

                Destroy(tree.gameObject);
            }
        }
    }
}