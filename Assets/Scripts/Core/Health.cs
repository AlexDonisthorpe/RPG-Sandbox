using UnityEngine;
using RPG.Saving;

namespace RPG.Core{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public object CaptureState()
        {
            return healthPoints;
        }

        public bool IsDead(){
            return isDead;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            TakeDamage(0);
        }

        public void TakeDamage(float damage){
            if(isDead) return;

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("dead");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}