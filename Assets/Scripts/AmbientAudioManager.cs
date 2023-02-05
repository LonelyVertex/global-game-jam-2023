using UnityEngine;

public class AmbientAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _ambientAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _gameOverAudioSource;
    [SerializeField] private AudioClip _loopMusicClip;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private Vector2 _ambientChance;
    
    void Start()
    {
        GameManager.Instance.OnGameOver += HandleOnGameOver;
        GameManager.Instance.OnBeforeStageChange += HandleOnBeforeStageChange;

        _musicAudioSource.Play();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }
        
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

    private void HandleOnGameOver()
    {
        _musicAudioSource.Stop();

        _gameOverAudioSource.Play();
    }

    private void HandleOnBeforeStageChange(int stage)
    {
        if (stage == 6)
        {
            _musicAudioSource.clip = _loopMusicClip;
            _musicAudioSource.loop = true;
            
            _musicAudioSource.Play(); 
        }
    }
}
