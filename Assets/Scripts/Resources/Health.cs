using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using RPG.Resources;
using System;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Resources{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] UnityEvent<float> takeDamage;

        LazyValue<float> healthPoints;
        float maxHealth;

        bool isDead = false;

        BaseStats baseStats;

        private void Awake() 
        {
            baseStats = GetComponent<BaseStats>();
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private void OnEnable() 
        {
            if (baseStats != null)
            {
                baseStats.onLevelUp += HealOnLevelUp;
            }
        }

        private void OnDisable() 
        {
            if (baseStats != null)
            {
                baseStats.onLevelUp -= HealOnLevelUp;
            }
        }

        private void Start() 
        {
            healthPoints.ForceInit();
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void HealOnLevelUp()
        {
            maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            if (healthPoints.value < maxHealth)
            {
                healthPoints.value = maxHealth;
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.Experience));
        }

        private void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("dead");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void TakeDamage(GameObject instigator, float damage){
            if(isDead) return;

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            takeDamage.Invoke(damage);


            if (healthPoints.value == 0)
            {
                AwardExperience(instigator);
                Die();
            } 
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            if(healthPoints.value <= 0)
            {
                Die();
            }
        }

        public float GetPercentage(){
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}