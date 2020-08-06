using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    public Transform nextWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyAi>().updateWaypoint(nextWaypoint);
        }
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Ok, we can do this");
        }
    }
}
