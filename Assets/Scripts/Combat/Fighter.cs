using UnityEngine;
using RPG.Movement;

namespace RPG.Combat{

public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        Mover mover;

        private void Start(){
            mover = GetComponent<Mover>();
        }

        private void Update(){
            bool isInRange = Vector3.Distance(gameObject.transform.position, target.position) <= weaponRange;
            
            if (target != null && !isInRange){
                    mover.MoveTo(target.position);
                } else {
                    mover.Stop();
                    target = null;
                }
            }

        public void Attack(CombatTarget combatTarget){
            target = combatTarget.transform;
        }
    }
}
