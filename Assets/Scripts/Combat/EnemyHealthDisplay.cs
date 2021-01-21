using System;
using UnityEngine;
using UnityEngine.UI;
using RPG.Combat;

namespace RPG.Attributes
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Health targetHealth;
        Text text;
        Fighter player;
        
        private void Awake() {
            player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            text = GetComponent<Text>();
        }

        private void Update() {
            targetHealth = player.GetTarget();
            if(targetHealth == null)
            {
                text.text = "N/A";
            } 
            else 
            {
                text.text = String.Format("{0:0}/{1:0}", targetHealth.GetHealthPoints().ToString(), targetHealth.GetMaxHealthPoints());
            }
        }
    }
}
