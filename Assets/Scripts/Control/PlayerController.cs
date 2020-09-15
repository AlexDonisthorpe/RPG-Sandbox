using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

[SerializeField] LayerMask layerToIgnore;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            moveToCursor();
        }
    }

    private void moveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Raycast as normal but ignore the defined layer, thanks!
        // TODO - I need to check the range of the raycast to make sure I'm not dropping
        //          any commands because they're just out of range...
        bool hasHit = Physics.Raycast(ray, out hit, 200f, ~layerToIgnore);
        if (hasHit)
        {
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}
