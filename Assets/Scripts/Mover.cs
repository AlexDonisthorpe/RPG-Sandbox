using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(1)){
            moveToCursor();
        }
    }

    private void moveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit){
            GetComponent<NavMeshAgent>().SetDestination(hit.point);
        }
    }
}
