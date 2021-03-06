﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	private int maxDistance;
	private Transform myTransform;

	void Awake ()
	{
		myTransform = transform;
	}

	// Use this for initialization
	void Start ()
	{
		GameObject go = GameObject.FindGameObjectWithTag ("Player");

		target = go.transform;

		maxDistance = 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.DrawLine (target.position, myTransform.position, Color.yellow);

		// Look at target
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, 
		                                         Quaternion.LookRotation (new Vector3 (target.position.x, myTransform.position.y, target.position.z) - myTransform.position), rotationSpeed * Time.deltaTime);

		// Move towards target
		if (Vector3.Distance (target.position, myTransform.position) > maxDistance)
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

	}
}
