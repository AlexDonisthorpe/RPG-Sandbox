using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        Text text;

        private void Awake() {
            text = GetComponent<Text>();
        }

        private void Update() {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            text.text = experience.GetPoints().ToString();
        }
    }
}