using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.2f;
        Vector3 previousChild;

        private void OnDrawGizmos() {
            
            Gizmos.color = Color.grey;
            previousChild = GetWaypoint(transform.childCount - 1);

            for(int i = 0; i < transform.childCount; i++)
            {
                Vector3 child = GetWaypoint(i);

                Gizmos.DrawSphere(child, waypointGizmoRadius);
                Gizmos.DrawLine(previousChild, child);

                previousChild = child;
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}