using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using RPG.Resources;
using System;

namespace RPG.Resources{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;
        float maxHealth;

        bool isDead = false;

        public bool IsDead(){
            return isDead;
        }

        private void Start() {
            if(healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
                maxHealth = healthPoints;
            }

            BaseStats baseStats = GetComponent<BaseStats>();
            if(baseStats != null)
            {
                baseStats.onLevelUp += HealOnLevelUp;
            }
        }

        private void HealOnLevelUp()
        {
            maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            if (healthPoints < maxHealth)
            {
                healthPoints = maxHealth;
            }
        }

        public void TakeDamage(GameObject instigator, float damage){
            if(isDead) return;

            print(gameObject.name + " took damage: " + damage);

            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                AwardExperience(instigator);
                Die();
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
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
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.Experience));
        }
    }
}