﻿using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 1f;
        [SerializeField] float lifeAfterImpact = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;

        Health target;
        float damage = 0;
        Vector3 targetTransform;

        private void Update()
        {
            if (target == null) return;

            if (targetTransform == Vector3.zero || isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsuleCollider = target.GetComponent<CapsuleCollider>();
            if (targetCapsuleCollider == null)
            {
                return target.transform.position;
            }

            targetTransform = target.transform.position + (Vector3.up * targetCapsuleCollider.height / 2);

            return targetTransform;
        }

        private void OnTriggerEnter(Collider other)
        {
            Health enemyHit = other.GetComponent<Health>();
            if (enemyHit != target) return;
            if (enemyHit.IsDead()) return;

            enemyHit.TakeDamage(damage);
            moveSpeed = 0;


            if (hitEffect != null) Instantiate(hitEffect, GetAimLocation(), transform.rotation);

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);

        }
    }
}