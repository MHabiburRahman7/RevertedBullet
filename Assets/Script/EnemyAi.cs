using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
    public Slider m_slider;
    public float health = 100f;
    public GameObject enemy_object;
    public float speed;

    public bool justInit = true, justTouchGround= false;
    public Transform targetWaypoint;

    private Transform decide_left, decide_right;
    private Vector3 _direction;
    private Quaternion _lookRotation;

    private void Awake()
    {
        //if (!player)
        //{
        //    player = GameObject.FindGameObjectWithTag("Player");
        //}
    }

    public void GetDamaged(int damage)
	{
        health -= damage;
	}
    

    void Update()
    {
        MoveForward();
        if (targetWaypoint != null)
        {
            //FacingPlayer();
            SpriteFace();
        }
        if (health <= 0) Destroy(gameObject);

        m_slider.value = health;
    }

    void SpriteFace()
    {
        if(transform.position.x - targetWaypoint.transform.position.x < 0)
        {
            enemy_object.transform.localScale = new Vector3(1, 1,1);
        }
        else
        {
            enemy_object.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void MoveForward()
    {
        if (!justInit)
        {
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (Vector3.Distance(gameObject.transform.position, targetWaypoint.transform.position) > 0.1f)
            {
                float step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);
            }
        }
        else if (justInit && justTouchGround)
        {
            //decide which one is closest
            if (Vector3.Distance(transform.position, decide_left.position) < Vector3.Distance(transform.position, decide_right.position))
                targetWaypoint = decide_left;
            else
                targetWaypoint = decide_right;

            justInit = false;
        }
    }

    void FacingPlayer()
    {
        _direction = (targetWaypoint.transform.position - gameObject.transform.position);

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //Quaternion newDir = Quaternion.Euler(_lookRotation.eulerAngles.x, 0, _lookRotation.eulerAngles.z);
        enemy_object.transform.rotation = new Quaternion(0, _lookRotation.y, 0, _lookRotation.w);

        //enemy.transform.rotation = newDir;
    }

    public void updateWaypoint(Transform t)
    {
        targetWaypoint = t;
    }

    public void InitWaypoint(Transform a, Transform b)
    {
        decide_left = a;
        decide_right = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            justTouchGround = true;
        }
    }
}
