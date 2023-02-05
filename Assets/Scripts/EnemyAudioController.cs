using System.Collections;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _movingAudioSource;
    [SerializeField] private AudioSource _oneShotAudioSource;

    [SerializeField] private AudioClip[] _attackClips;
    [SerializeField] private AudioClip[] _moveClips;
    [SerializeField] private AudioClip[] _takeHitClips;
    [SerializeField] private AudioClip[] _dieClips;
    [SerializeField] private AudioClip[] _afterStage5DieClips;

    
    public void StartMoving()
    {
        if (_movingAudioSource.isPlaying)
        {
            return;
        }

        StartCoroutine(Moving());
    }

    public void StopMoving()
    {
        if (!_movingAudioSource.isPlaying)
        {
            return;
        }
        
        _movingAudioSource.Stop();
        StopCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        _movingAudioSource.clip = _moveClips[Random.Range(0, _moveClips.Length)];
        _movingAudioSource.Play();

        while (true)
        {
            if (!_movingAudioSource.isPlaying)
            {
                _movingAudioSource.clip = _moveClips[Random.Range(0, _moveClips.Length)];
                _movingAudioSource.Play();
            }
            
            yield return null;
        }
    } 

    public void TakeHit()
    {
        _oneShotAudioSource.PlayOneShot(_takeHitClips[Random.Range(0, _takeHitClips.Length)]);
    }

    public void Die()
    {
        if (GameManager.Instance.CurrentStage >= 5)
        {
            _oneShotAudioSource.PlayOneShot(_afterStage5DieClips[Random.Range(0, _afterStage5DieClips.Length)]);
        }
        else
        {
            _oneShotAudioSource.PlayOneShot(_dieClips[Random.Range(0, _dieClips.Length)]);
        }
    }

    public void Attack()
    {
        _oneShotAudioSource.PlayOneShot(_attackClips[Random.Range(0, _attackClips.Length)]);
    }
}
