using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas canvas = null;

        void Update()
        {
            float healthFraction = healthComponent.GetFraction();
            if (Mathf.Approximately(healthFraction, 0) || Mathf.Approximately(healthFraction, 1))
            {
                canvas.enabled = false;
                return;
            }

            canvas.enabled = true;
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1);
        }
    }
}
