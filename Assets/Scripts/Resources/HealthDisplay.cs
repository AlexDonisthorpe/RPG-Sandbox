using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        Text text;
        
        private void Awake() {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            text = GetComponent<Text>();
        }

        private void Update() {
            text.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints().ToString(), health.GetMaxHealthPoints());
        }
    }
}
