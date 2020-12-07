using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using RPG.Resources;
using System;

namespace RPG.Resources{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        float maxHealth;

        bool isDead = false;

        public bool IsDead(){
            return isDead;
        }

        private void Start() {
            healthPoints = GetComponent<BaseStats>().GetHealth();
            maxHealth = healthPoints;
        }

        public void TakeDamage(GameObject instigator, float damage){
            if(isDead) return;

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                AwardExperience(instigator);
                Die();
            }
        }



        private void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("dead");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if(healthPoints <= 0)
            {
                Die();
            }
        }

        public float GetPercentage(){
            return 100 * (healthPoints / GetComponent<BaseStats>().GetHealth());
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetExperienceReward());
        }
    }
}