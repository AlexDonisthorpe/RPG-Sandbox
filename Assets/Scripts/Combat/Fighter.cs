using UnityEngine;

namespace RPG.Combat{

public class Fighter : MonoBehaviour
    {
        public void Attack(CombatTarget target){
            Debug.Log(string.Format("*Slap* {0}", target.gameObject.name));
        }
    }
}
