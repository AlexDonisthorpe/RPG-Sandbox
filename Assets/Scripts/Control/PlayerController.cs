using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] LayerMask layerToIgnore;
        [SerializeField] float maxNavMeshProjectionDistance = 4f;
        [SerializeField] float maxNavPathLength = 200f;

        private void Awake() {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if(InteractWithUI()) return;
            if(health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }
            if(InteractWithComponent()) return;
            if(InteractWithMovement()) return;

            SetCursor(CursorType.None);
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach(RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables)
                {
                    if(raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];

            for (int i = 0; i<hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private bool InteractWithUI()
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    Debug.Log(target);
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit rayHit;
            NavMeshHit navMeshHit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out rayHit);
            if (!hasHit) return false;



            bool hasCastToNavMesh = NavMesh.SamplePosition(
                rayHit.point, out navMeshHit, maxNavMeshProjectionDistance,
                    NavMesh.AllAreas);
            if(!hasCastToNavMesh) return false;


            target = navMeshHit.position;

            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);

            if(!hasPath) return false;

            if(path.status != NavMeshPathStatus.PathComplete) return false;
            if(GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float pathLength = 0;

            if(path.corners.Length < 2) return pathLength;

            for(int i = 0; i<path.corners.Length - 1; i++)
            {
                pathLength += Vector3.Distance(path.corners[i], path.corners[i+1]);
            }

            return pathLength;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach(CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }

            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
