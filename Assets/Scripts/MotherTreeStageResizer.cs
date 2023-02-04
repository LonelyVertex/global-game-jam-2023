using System;
using System.Linq;
using UnityEngine;

public class MotherTreeStageResizer : StageResizer
{
    [SerializeField] LifeForceGenerator lifeForceGenerator;
    [SerializeField] CircleCollider2D circleCollider2D;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, circleCollider2D.radius * transform.localScale.x);
    }

    protected override void Update()
    {
        base.Update();

        if (IsResized()) return;
        DevourTrees();
    }

    void DevourTrees()
    {
        var trees = FindObjectsOfType<TreeBehaviour>()
            .Where(tree => Vector3.Distance(tree.transform.position, transform.position) <= circleCollider2D.radius * transform.localScale.x);
            
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