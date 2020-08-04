using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCtrl : MonoBehaviour
{
    public Vector3 worldPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            //transform.LookAt(hit.point);

            worldPosition = hit.point + Vector3.up;
        }

        //Debug.Log("Mouse pos : " + worldPosition);
    }
}
