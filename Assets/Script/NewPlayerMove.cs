using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMove : MonoBehaviour
{
	public int healthVal;
	public float speed, bulletSpeed;
	public int bullet_num;

	private float input_x;
	private float input_y;

	//see to the mouse
	//values that will be set in the Inspector
	public Transform AimSprite;
	public float jumpForce, RotationSpeed;
	public AimCtrl m_aimCtrl;
	public GameObject bulletPrefab, front;
	public bool isGrounded = false;


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
		justJump();

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
		for (int i = 0; i < bulletAway_gameObject.Count; i++)
		{
			//Debug.Log("Pulling bullet: " + i);
			bulletAway_gameObject[i].GetComponent<BulletCtrl>().pullTheBullet();
		}
	}

	void playerMove()
	{
		input_x = Input.GetAxisRaw("Horizontal");
		input_y = Input.GetAxisRaw("Vertical");

		transform.position += new Vector3(input_x * speed, 0, 0) * Time.deltaTime;
	}


	void playerFace()
	{

		// find the vector pointing from our position to the target
		//_direction = (m_aimCtrl.worldPosition - transform.position);
		_direction = (m_aimCtrl.worldPosition - front.transform.position).normalized;

		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation(_direction);

		//_direction = new Vector3(0, 0, _direction.z);
		//front.transform.LookAt(_direction, front.transform.forward);
		//front.transform.up = _direction;

		//rotate us over time according to speed until we are in the required rotation
		front.transform.rotation = Quaternion.Slerp(front.transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		//Quaternion LookAtRotationOnly_Z = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, _lookRotation.eulerAngles.z);

		//Debug.Log("euler angels before: " + _lookRotation.eulerAngles);
		//_lookRotation.eulerAngles = new Vector3(0, 0, _lookRotation.eulerAngles.z);
		//Debug.Log("euler angels: " + _lookRotation.eulerAngles);
		//front.transform.rotation = _lookRotation;
		//front.transform.up= _lookRotation.eulerAngles.z;

		//transform.rotation = new Quaternion(0, 0, _lookRotation.z, _lookRotation.w);
		//transform.rotation = Quaternion.Slerp(transform.rotation, LookAtRotationOnly_Z, Time.deltaTime * RotationSpeed);


	}

	void justJump()
	{
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			//Debug.Log("Going to jump!");
			//go up
			Vector3 forc = new Vector3(0, jumpForce, 0f);

			rb.AddForce(forc);
		}
	}

	void AimFace()
	{
		AimSprite.transform.position = m_aimCtrl.worldPosition;
	}

	void fireBullet()
	{
		Vector3 direction = m_aimCtrl.worldPosition - front.transform.position;
		direction.z = 0f;

		//Debug.Log("Velocity: " + direction);
		GameObject projectile = (GameObject)Instantiate(bulletPrefab, front.transform.position, Quaternion.identity);
		bulletAway_gameObject.Add(projectile);
		projectile.GetComponent<BulletCtrl>().SetupBullet(direction, bulletSpeed, gameObject.transform.position);
		bulletAway = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			healthVal -= 3;
		}
	}

	//private void OnCollisionEnter(Collision other)
	//{
	//	if (other.gameObject.tag == "Enemy")
	//	{
	//		healthVal -= 3;
	//	}
	//}

	private void OnCollisionStay(Collision other)
	{
		if (other.gameObject.tag == "Ground")
		{
			//allowed to jump
			isGrounded = true;
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Ground")
		{
			//allowed to jump
			isGrounded = false;
		}
	}
}
