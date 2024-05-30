using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Signaling : MonoBehaviour
{
    private AudioSource _audioSource;
    private Coroutine _coroutine;
    private float _stepVolume = 0.02f;

    public float MaxVolume { get; private set; } = 1.0f;
    public float MinVolume { get; private set; } = 0f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = MinVolume;
    }

    public void Playback(float requiredVolume)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeVolume(requiredVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        while (!Mathf.Approximately(_audioSource.volume, targetVolume))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _stepVolume * Time.deltaTime);
            yield return null;
        }

        if (Mathf.Approximately(_audioSource.volume, MinVolume))
        {
            _audioSource.Stop();
        }
    }
}