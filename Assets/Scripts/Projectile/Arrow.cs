using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile {

    private Transform _target;
    private Vector2 _direction;
    private Rigidbody2D _rb;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _damage = 1f;
    private float _liveSpan = 1f;


    private void Awake () {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate () {
        if (_target != null) {
            Fly();

        }
        CheckLiveSpan();
    }

    private void Fly () {
        Rotate();
        _direction = (_target.position - transform.position).normalized;
        _rb.velocity = _direction * _speed;
    }

    private void CheckLiveSpan () {
        _liveSpan -= Time.deltaTime;
        if (_liveSpan <= 0) {
            Destroy(gameObject);
        }
    }

    public void SetTarget (Transform target) {
        _target = target;
    }

    public void Rotate () {
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        IDamagable damagable = collision.collider.GetComponent<IDamagable>();
        damagable.TakeDamage(_damage);
        Destroy(gameObject);
    }
}
