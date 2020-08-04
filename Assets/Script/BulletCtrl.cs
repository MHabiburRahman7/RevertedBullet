using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public Vector3 target, player_lastPosition;
    public float init_speed;
    private Rigidbody _rb;
    public bool isPulled, isClose;
    public int Damage;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        Invoke("LaunchBullet", 0.2f);
    }

    void LaunchBullet()
    {
        _rb.velocity = target * init_speed;
    }

    public void SetupBullet(Vector3 target_pos, float speed, Vector3 p)
    {
        target = target_pos;
        init_speed = speed;
        player_lastPosition = p;

        isPulled = false;
        isClose = false;
    }

    public void pullTheBullet()
    {
        isPulled = true;
        _rb.velocity = new Vector3(0f,0f,0f);
    }

    void moveBackTothePlayer()
    {//WRONG
     //if it is close enough
        float dist = Vector3.Distance(gameObject.transform.position, player_lastPosition);
        if (Vector3.Distance(gameObject.transform.position, player_lastPosition) > 0.1f)
        {
            // Move our position a step closer to the target.
            float step = init_speed * Time.deltaTime * 10; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, player_lastPosition, step);
        }else if(Vector3.Distance(gameObject.transform.position, player_lastPosition) <= 1f)
        {
            isClose = true;
        }

        //Debug.Log("Distance: " + dist);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPulled)
        {
            moveBackTothePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Enemy Hit");
            other.gameObject.GetComponent<EnemyAi>().GetDamaged(1);
        }
    }
}
