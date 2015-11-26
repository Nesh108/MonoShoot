using UnityEngine;
using System.Collections;

public class HackAndSlashCamera : MonoBehaviour
{
	public Transform target;
	public float runDistance;
	public float walkDistance;
	public float height;
	private Transform _myTransform;

	private GameObject _player;

	void Start ()
	{
		if (target == null) {
			Debug.LogWarning ("No target attached to camera. Using player character instead, mate.");
			_player = GameObject.FindGameObjectWithTag("Player") as GameObject;
			target = _player.transform;
		}
	
		_myTransform = transform;
	}

	void Update ()
	{

	}

	void LateUpdate ()
	{
		_myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
		_myTransform.LookAt(target);
	}
}