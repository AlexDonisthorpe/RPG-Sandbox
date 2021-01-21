using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        Text textField;

        private void Awake() 
        {
            textField = GetComponentInChildren<Text>();    
        }

        public void SetValue(float value)
        {
            textField.text = String.Format("{0:0}", value);
        }
    }
}
