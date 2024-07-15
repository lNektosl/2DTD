using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable {
    public static event Action OnEnemyDied;

    [Header("Attributes")]
    [SerializeField] private float _moveSpeed = 2f;

    [SerializeField] private float _health = 2f;


    private Rigidbody2D _rb;
    private Transform _target;
    private int _pathIndex = 0;
    private Vector2 _direction;
    private Vector2 _lastPosition;
    private bool _isDead = false;

    private void Awake () {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start () {
        _rb.freezeRotation = true;
        UpdateTarget();
    }
    private void Update () {
        if (Vector2.Distance(_target.position, transform.position) <= 0.1f) {
            _pathIndex++;
            if (_pathIndex == LevelManager.main.path.Length) {
                Die();
                return;
            }
            UpdateTarget();
        }
    }

    private void FixedUpdate () {
        Move();
    }

    private void UpdateTarget () {
        _target = LevelManager.main.path[_pathIndex];
        GetDirection();
    }

    #region Movment
    private void GetDirection () {
        _direction = (_target.position - transform.position).normalized;
    }
    private void Move () {
        float step = _moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _target.position, step);
    }

    #endregion

    #region Health

    public void TakeDamage (float damage) { 
        _health -= damage;
        if (_health <= 0 && !_isDead) {
            Die();
        }
    }

    public void Die () {
        _isDead = true;
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }

    #endregion

    private void OnCollisionEnter2D (Collision2D collision) {
        
    }
}
