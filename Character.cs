using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator> ();
        _spriteRenderer= GetComponent<SpriteRenderer> ();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.Translate(movement * _speed * Time.deltaTime, Space.World);
        HandleMovement(movement);
    }

    private void HandleMovement(Vector3 movement)
    {
        float speed = movement.magnitude;
        _animator.SetFloat("Speed", speed);

        if (speed > 0)
        {
            _spriteRenderer.flipX = movement.x < 0;
        }
    }
}
