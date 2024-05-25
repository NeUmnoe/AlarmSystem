using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]

public class AlarmSystem : MonoBehaviour
{
    private AudioSource _audioSource;
    private Coroutine _coroutineActiveSound;
    private float MaxVolume = 1.0f;
    private float MinVolume = 0f;
    private float StepVolume = 0.02f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = MinVolume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            Playback(MaxVolume);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            Playback(MinVolume);
        }
    }

    private void Playback(float requiredVolume)
    {
        if (_coroutineActiveSound != null)
        {
            StopCoroutine(_coroutineActiveSound);
        }

        _coroutineActiveSound = StartCoroutine(ChangeVolume(requiredVolume));
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        while (!Mathf.Approximately(_audioSource.volume, targetVolume))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, StepVolume * Time.deltaTime);
            yield return null;
        }

        if (Mathf.Approximately(_audioSource.volume, MinVolume))
        {
            _audioSource.Stop();
        }
    }
}