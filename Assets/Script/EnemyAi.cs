using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public int health = 2;
    public GameObject player, enemy;
    public float speed;

    private Vector3 _direction;
    private Quaternion _lookRotation;

    private void Awake()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void GetDamaged(int damage)
	{
        health -= damage;
	}
    

    void Update()
    {
        MoveForward();
        FacingPlayer();
        if (health == 0) Destroy(gameObject);
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void FacingPlayer()
    {
        _direction = (player.transform.position - gameObject.transform.position);

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //Quaternion newDir = Quaternion.Euler(_lookRotation.eulerAngles.x, 0, _lookRotation.eulerAngles.z);
        enemy.transform.rotation = new Quaternion(0, _lookRotation.y, 0, _lookRotation.w);

        //enemy.transform.rotation = newDir;
    }
}
