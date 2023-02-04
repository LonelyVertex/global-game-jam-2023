using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float attackCooldown;
    [SerializeField] float radius;

    int _currentHealth;
    float _lastAttack;
    TreeBehaviour _target;
    bool _isInRange;

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Start()
    {
        _currentHealth = maxHealth;
    }

    void Update()
    {
        CheckIsInRange();
        SearchTarget();

        if (!_target) return;
        if (_isInRange)
        {
            Attack();
        }
        else
        {
            Walk();
        }
    }

    void SearchTarget()
    {
        if (_isInRange) return;

        var trees = GameObject.FindObjectsOfType<TreeBehaviour>()
            .OrderBy((tree) => Vector3.Distance(transform.position, tree.transform.position))
            .ToArray();

        if (trees.Length > 0)
        {
            _target = trees[0];
        }
    }

    void CheckIsInRange()
    {
        if (!_target)
        {
            _isInRange = false;
        }
        else
        {
            var distance = Vector3.Distance(transform.position, _target.transform.position);
            _isInRange = distance <= _target.Radius + radius;
        }
    }

    void Attack()
    {
        if (Time.time - _lastAttack >= attackCooldown)
        {
            _target.TakeDamage(damage);
            _lastAttack = Time.time;
        }
    }

    void Walk()
    {
        var targetPosition = _target.transform.position;
        var direction = (targetPosition - transform.position).normalized;

        transform.right = direction;
        transform.Translate(direction * (speed * Time.deltaTime), Space.World);
    }
}