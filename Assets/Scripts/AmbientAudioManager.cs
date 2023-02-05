using UnityEngine;

public class AmbientAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _ambientAudioSource;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private Vector2 _ambientChance;

    void Update()
    {
        if (_ambientAudioSource.isPlaying)
        {
            return;
        }
        
        PickSound();
    }

    private void PickSound()
    {
        var val = Random.Range(0, 100);
        if (!(val > _ambientChance.x && val < _ambientChance.y))
        {
            return;
        }

        _ambientAudioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
        _ambientAudioSource.Play();
    }
}
