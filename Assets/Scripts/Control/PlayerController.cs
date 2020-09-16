using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] LayerMask layerToIgnore;

        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits){
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if(Input.GetMouseButtonDown(1)){
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(1))
            {
                moveToCursor();
            }
        }

        private void moveToCursor()
        {
            RaycastHit hit;

            // Raycast as normal but ignore the defined layer, thanks!
            // TODO - I need to check the range of the raycast to make sure I'm not dropping
            //          any commands because they're just out of range...
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit, 200f, ~layerToIgnore);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
