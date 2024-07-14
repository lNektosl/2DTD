using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowTower : MonoBehaviour {

    [SerializeField] private LayerMask _enemyMask;

    [SerializeField] private float _range = 2.5f;
    [SerializeField] private float _fireRate = 1f;

    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectile;

    private Transform _target;
    private bool _isReadyToFire = true;


    private void FixedUpdate () {
        if (_target == null) {
            FindTarget();
            return;
        }
        if (!CheckTargetIsInRange()) {
            _target = null;
        }

        Shoot();


    }

    private void Shoot () {
        if (_isReadyToFire && _target !=null) {

            GameObject projectileObj = Instantiate(_projectile, _firePoint.position, Quaternion.identity);
            IProjectile projectile = projectileObj.GetComponent<IProjectile>();
            projectile.SetTarget(_target);
            _isReadyToFire = false;
            StartCoroutine("Reload");
        }
    }

    private IEnumerator Reload () {
        yield return new WaitForSeconds(1f / _fireRate);
        _isReadyToFire = true;
    }

    private void OnDrawGizmosSelected () {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.forward, _range);
    }

    private void FindTarget () {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _range, Vector2.zero, 0f, _enemyMask);

        if (hits.Length > 0) {
            _target = hits[0].transform;
        }
    }
    private bool CheckTargetIsInRange () {
        return Vector2.Distance(transform.position, _target.position) <= _range;
    }
}
