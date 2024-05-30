using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class IntruderDetector : MonoBehaviour
{
    [SerializeField] private Signaling _signaling;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            _signaling.Playback(_signaling.MaxVolume);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            _signaling.Playback(_signaling.MinVolume);
        }
    }
}