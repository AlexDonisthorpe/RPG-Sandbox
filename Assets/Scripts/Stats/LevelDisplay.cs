using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        Text text;
        int level;

        private void Start() {
            text = GetComponent<Text>();
        }
        // Update is called once per frame
        void Update()
        {
            level = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>().GetLevel();
            text.text = level.ToString();
        }
    }
}
