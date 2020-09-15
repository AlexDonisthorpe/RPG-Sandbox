using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement{
    public class Mover : MonoBehaviour
    {
        void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 hit)
        {
            GetComponent<NavMeshAgent>().SetDestination(hit);
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}