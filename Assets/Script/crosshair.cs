using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshair : MonoBehaviour
{
    public GameObject Cam;

    void Update()
    {
        transform.LookAt(Cam.transform);
    }
}
