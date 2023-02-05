using System.Linq;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float attackCooldown;
    [SerializeField] CircleCollider2D circleCollider2D;
    [SerializeField] Animator _animator;
    [SerializeField] EnemyAudioController _enemyAudioController;
    [SerializeField] ParticleSystem _dieParticleSystem;

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
        
        _enemyAudioController.TakeHit();

        if (_currentHealth <= 0)
        {
            _enemyAudioController.Die();
            _animator.SetTrigger("Die");
            Instantiate(_dieParticleSystem, transform.position, transform.rotation);
            
            Destroy(gameObject, 0.167f);
        }
    }

    void Start()
    {
        _currentHealth = maxHealth;
    }

    void Update()
    {
        if (+_currentHealth <= 0)
        {
            return;
        }
        
        CheckIsInRange();
        SearchTarget();

        _animator.SetBool("Attacking", false);
        
        if (!_target) return;
        if (_isInRange)
        {
            Attack();
            
            _enemyAudioController.StopMoving();
        }
        else
        {
            Walk();
            
            _enemyAudioController.StartMoving();
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
            _isInRange = distance <= _target.Radius + circleCollider2D.radius;
        }
    }

    void Attack()
    {
        if (Time.time - _lastAttack >= attackCooldown)
        {
            _target.TakeDamage(damage);
            _lastAttack = Time.time;
            
            _enemyAudioController.Attack();
        }
        
        _animator.SetBool("Attacking", true);
    }

    void Walk()
    {
        var targetPosition = _target.transform.position;
        var direction = (targetPosition - transform.position).normalized;
        var baseSpeed = speed * GameManager.Instance.EnemySpeedModifier;
        var movementSpeed = _slowModifiers > 0 ? baseSpeed * GameManager.Instance.SlowModifier : baseSpeed;

        transform.right = direction;
        transform.Translate(direction * (movementSpeed * Time.deltaTime), Space.World);
        
        _animator.SetFloat("Speed", movementSpeed);
    }
}