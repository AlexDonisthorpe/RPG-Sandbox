using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform target;

private void Update() {
    if(target == null) return;

    transform.LookAt(GetAimLocation());
    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
}

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsuleCollider = target.GetComponent<CapsuleCollider>();
        if(targetCapsuleCollider == null)
        {
            return target.position;
        }
        return target.position + (Vector3.up * targetCapsuleCollider.height / 2);
    }
}
