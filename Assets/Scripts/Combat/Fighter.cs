using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat{

public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        float timeSinceLastAttack = 1;
        Health target;
        Mover mover;

        private void Start(){
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            bool isInAttack = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack");

            if (target == null || target.IsDead()) return;

            if (!GetIsInRange())
            {
                if (isInAttack)
                {
                    GetComponent<Animator>().SetTrigger("attackCancel");
                }
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("attackCancel");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(gameObject.transform.position, target.transform.position) <= weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("attackCancel");
        }

        // Hit event is triggered during the attack animation
        void Hit(){
            if(target == null) return;
            target.TakeDamage(weaponDamage);
        }
    }
}
