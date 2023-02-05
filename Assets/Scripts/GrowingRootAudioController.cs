using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingRootAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;

    public void StartGrowing()
    {
        _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Length)]);
    }

    public void StopGrowing()
    {
        _audioSource.Stop();
    }
}
