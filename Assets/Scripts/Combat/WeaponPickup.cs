using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag != "Player") return;

            other.GetComponent<Fighter>().EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            SetVisible(false);
            yield return new WaitForSeconds(seconds);
            SetVisible(true);
        }

        private void SetVisible(bool setting)
        {
            GetComponent<CapsuleCollider>().enabled = setting;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(setting);
            }
        }
    }
}