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
    [SerializeField] Animator _animator;

    int _currentHealth;
    float _lastAttack;
    TreeBehaviour _target;
    bool _isInRange;
    int _slowModifiers;

    public void AddSlowModifier()
    {
        _slowModifiers++;
    }

    public void RemoveSlowModifier()
    {
        _slowModifiers = Mathf.Max(0, _slowModifiers - 1);
    }

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

        _animator.SetBool("Attacking", false);
        
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

        var trees = FindObjectsOfType<TreeBehaviour>()
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
        
        _animator.SetBool("Attacking", true);
    }

    void Walk()
    {
        var targetPosition = _target.transform.position;
        var direction = (targetPosition - transform.position).normalized;
        var movementSpeed = _slowModifiers > 0 ? speed * GameManager.Instance.SlowModifier : speed;

        transform.right = direction;
        transform.Translate(direction * (movementSpeed * Time.deltaTime), Space.World);
        
        _animator.SetFloat("Speed", movementSpeed);
    }
}