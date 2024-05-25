using UnityEngine;

[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]

public class Character : MonoBehaviour
{
    const string SpeedParameter = "Speed";
    const string VerticalParameter = "Vertical";
    const string HorizontalParameter = "Horizontal";

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
        float moveHorizontal = Input.GetAxis(HorizontalParameter);
        float moveVertical = Input.GetAxis(VerticalParameter);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        transform.Translate(movement * _speed * Time.deltaTime, Space.World);
        HandleMovement(movement);
    }

    private void HandleMovement(Vector3 movement)
    {
        float speed = movement.magnitude;

        _animator.SetFloat(SpeedParameter, speed);

        if (speed > 0)
        {
            _spriteRenderer.flipX = movement.x < 0;
        }
    }
}
