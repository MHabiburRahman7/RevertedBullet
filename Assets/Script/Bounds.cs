using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public Transform playerInit;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = playerInit.position;
        }
    }
}
