using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 7f;
    private Vector2 _direction;

    [Space]
    [SerializeField] [Min(0)] private int _extraJumps = 1;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private CapsuleCollider2D capsuleCollider2D;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private Transform _transform;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Move()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        _rigidbody2D.velocity = new Vector2(_direction.x * _moveSpeed, _rigidbody2D.velocity.y);
    }

    void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded =>
        Physics2D.Linecast(
            _transform.position,
            _collider2D.bounds.min - new Vector3(0, 0.1f, 0),
            _contactFilter,
            new List<RaycastHit2D>()) > 1;
}
