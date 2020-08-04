using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public int healthVal;
	public float speed, bulletSpeed;
	public int bullet_num;

	private float input_x;
	private float input_y;

	//see to the mouse
	//values that will be set in the Inspector
	public Transform AimSprite;
	public float RotationSpeed;
	public float jumpForce;
	public AimCtrl m_aimCtrl;
	public GameObject bulletPrefab, front;
	private Rigidbody rb;
	public List<GameObject> bulletAway_gameObject;

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;
	private bool bulletAway;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		bulletAway = false;
		healthVal = 100;
	}

	void Update()
	{
		playerMove();
		playerFace();
		AimFace();

		if (Input.GetMouseButtonDown(0) && !bulletAway)
		{
			fireBullet();
		}
		if (bulletAway && Input.GetMouseButtonDown(1))
		{
			pullTheBullet();
		}
		if (bulletAway)
		{
			checkBulletLoc();
			if (bulletAway_gameObject.Count == 0)
				bulletAway = false;
		}
	}

	void checkBulletLoc()
	{
		for (int i = 0; i < bulletAway_gameObject.Count; i++)
		{
			if (bulletAway_gameObject[i].GetComponent<BulletCtrl>().isClose == true)
			{
				//Debug.Log("Bullet" + i + " is pulled");
				GameObject tempBullet = bulletAway_gameObject[i];
				bulletAway_gameObject.RemoveAt(i);
				Destroy(tempBullet);
			}
		}
	}

	void pullTheBullet()
	{
		for(int i=0; i<bulletAway_gameObject.Count; i++)
		{
			//Debug.Log("Pulling bullet: " + i);
			bulletAway_gameObject[i].GetComponent<BulletCtrl>().pullTheBullet();
		}
	}

	void playerMove()
	{
		input_x = Input.GetAxisRaw("Horizontal");
		input_y = Input.GetAxisRaw("Vertical");

		transform.position += new Vector3(input_x * speed, 0, input_y * speed).normalized * Time.deltaTime;
	}


	void playerFace()
	{

		// find the vector pointing from our position to the target
		//_direction = (m_aimCtrl.worldPosition - transform.position);
		_direction = (m_aimCtrl.worldPosition - front.transform.position).normalized;

		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);

		//_direction = new Vector3(0, 0, _direction.z);
		//transform.LookAt(_direction, transform.forward);
		//transform.up = _direction;

		//rotate us over time according to speed until we are in the required rotation
		//transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		//Quaternion LookAtRotationOnly_Z = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, _lookRotation.eulerAngles.z);

		//Debug.Log("euler angels before: " + _lookRotation.eulerAngles);
		//_lookRotation.eulerAngles = new Vector3(0, 0, _lookRotation.eulerAngles.z);
		//Debug.Log("euler angels: " + _lookRotation.eulerAngles);
		transform.rotation = _lookRotation;

		//transform.rotation = new Quaternion(0, 0, _lookRotation.z, _lookRotation.w);
		//transform.rotation = Quaternion.Slerp(transform.rotation, LookAtRotationOnly_Z, Time.deltaTime * RotationSpeed);
	}

	void AimFace()
	{
		AimSprite.transform.position = m_aimCtrl.worldPosition;
	}

	void fireBullet()
	{
		Vector3 direction = m_aimCtrl.worldPosition - front.transform.position;
		direction.y = 0f;

		//Debug.Log("Velocity: " + direction);
		GameObject projectile = (GameObject)Instantiate(bulletPrefab, front.transform.position, Quaternion.identity);
		bulletAway_gameObject.Add(projectile);
		projectile.GetComponent<BulletCtrl>().SetupBullet(direction, bulletSpeed, gameObject.transform.position);
		bulletAway = true;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			healthVal-=3;
		}
	}
}
