﻿using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] LayerMask layerToIgnore;

        Health health;

        private void Start() {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if(health.IsDead()) return;
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits){
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if(Input.GetMouseButton(1)){
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;

            // Raycast as normal but ignore the defined layer, thanks!
            // TODO - I need to check the range of the raycast to make sure I'm not dropping
            //          any commands because they're just out of range...
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit, 200f, ~layerToIgnore);
            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
