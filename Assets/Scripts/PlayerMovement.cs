using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float _inputX = Input.GetAxisRaw("Horizontal");
        float _inputY = Input.GetAxisRaw("Vertical");

        _animator.SetFloat("Move", Math.Abs(_inputX) + Math.Abs(_inputY));

        if (_inputX < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if (_inputX > 0)
        {
            _spriteRenderer.flipX = false;
        }

        Vector2 velocity = new Vector2(_inputX, _inputY) * _speed;

        _rigidbody.velocity = velocity;
    }
}
