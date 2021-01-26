using UnityEngine;
using RPG.Attributes;
using RPG.Stats;
using UnityEngine.Events;

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
        [SerializeField] UnityEvent projectileHit; 

        Health target;
        float damage = 0;
        GameObject instigator;
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

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
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

            projectileHit.Invoke();
            enemyHit.TakeDamage(instigator, damage);
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
