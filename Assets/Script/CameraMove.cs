using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public GameObject followTarget;
	public Vector3 targetPos;
	public float moveSpeed;

	public static bool cameraExist;

	// Use this for initialization
	void Start()
	{

		if (cameraExist)
		{
			Destroy(gameObject);
		}
		else
		{
			cameraExist = true;
		}
	}

	// Update is called once per frame
	void Update()
	{
		targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
