using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpFX = null;
        [SerializeField] bool shouldUseModifiers = false;

        int currentLevel = 0;

        public event Action onLevelUp;

        private void Start() {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel() {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                Debug.Log("Levelling Up");
                onLevelUp();
                LevelUpEffect();
            }
        }

        private void LevelUpEffect()
        {
            var effect = Instantiate(levelUpFX, transform);
        }

        public float GetStat(Stat stat)
        {
            return GetBaseStat(stat) + GetAdditiveModifier(stat) * (1 + GetPercentageModifier(stat)/100);
        }



        public int GetLevel()
        {
            if(currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if(experience == null) return startingLevel;

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for(int level = 1; level < penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(XPToLevelUp > currentXP) return level;

            }
            return penultimateLevel + 1;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if(!shouldUseModifiers) return 0;
            float totalModifiers = 0;

            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float modifier in provider.GetAdditiveModifiers(stat))
                {
                    totalModifiers += modifier;
                }
            }
            return totalModifiers;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if(!shouldUseModifiers) return 0;
            float totalPercentage = 0;

            foreach(IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach(float percentage in provider.GetPercentageModifiers(stat))
                {
                    totalPercentage += percentage;
                }
            }

            return totalPercentage;
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }
    }
}