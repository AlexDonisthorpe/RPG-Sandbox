using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat{

public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        Mover mover;

        private void Start(){
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (target == null) return;

            if (target != null && !GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(gameObject.transform.position, target.position) <= weaponRange;
        }

        public void Attack(CombatTarget combatTarget){
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel(){
            target = null;
        }

        // Animation Event
        void Hit(){

        }
    }
}
