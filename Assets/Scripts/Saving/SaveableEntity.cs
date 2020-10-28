using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {

        [SerializeField] string uniqueIdentifier = "";

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {

            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            NavMeshAgent navMesh = GetComponent<NavMeshAgent>();
            SerializableVector3 newPosition = (SerializableVector3)state;

            navMesh.enabled = false;
            transform.position = newPosition.ToVector();
            navMesh.enabled = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

# if UNITY_EDITOR
        private void Update() 
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;
            
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

            print("Editing...");

            if(string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}