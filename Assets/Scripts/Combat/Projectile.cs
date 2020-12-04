using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float destroyDelay = 0.2f;
    [SerializeField] bool isHoming = false;

    Health target;
    float damage = 0;
    Vector3 targetTransform;

    private void Update() 
    {
        if(target == null) return;

        if(targetTransform == Vector3.zero || isHoming && !target.IsDead())
        {
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsuleCollider = target.GetComponent<CapsuleCollider>();
        if(targetCapsuleCollider == null)
        {
            return target.transform.position;
        }

        targetTransform = target.transform.position + (Vector3.up * targetCapsuleCollider.height / 2);

        return targetTransform;
    }

    private void OnTriggerEnter(Collider other) {
        Health enemyHit = other.GetComponent<Health>();
        if(enemyHit != target) return;
        if(enemyHit.IsDead()) return;

        enemyHit.TakeDamage(damage);
        Destroy(gameObject, destroyDelay);

    }
}
