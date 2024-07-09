using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour {


    [Header("Attributes")]
    [SerializeField] private float _moveSpeed = 2f;


    private Rigidbody2D _rb;
    private Transform _target;
    private int _pathIndex = 0;
    private Vector2 _direction;


    private void Awake () {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start () {
        UpdateTarget();
    }
    private void Update () {
        if (Vector2.Distance(_target.position, transform.position) <= 0.1f) {
            _pathIndex++;
            if (_pathIndex == LevelManager.main.path.Length) {
                Destroy(gameObject);
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
        _rb.velocity = _direction * _moveSpeed;
    }

    #endregion
}
