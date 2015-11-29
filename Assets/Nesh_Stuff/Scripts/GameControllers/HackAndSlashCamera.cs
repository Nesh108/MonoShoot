using UnityEngine;
using System.Collections;

public class HackAndSlashCamera : MonoBehaviour
{
	public Transform target;
	public float runDistance;
	public float walkDistance;
	public float zoomStep = 0.40f;
	public float height;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float yMinLimit = 10f;
	public float yMaxLimit = 60f;
	private float _x;
	private float _y;
	private Transform _myTransform;
	private GameObject _player;
	private bool _camButtonDown = false;

	void Awake ()
	{
		_myTransform = transform;
	}

	void Start ()
	{
		if (target == null) {
			Debug.LogWarning ("No target attached to camera. Using player character instead, mate.");
			_player = GameObject.FindGameObjectWithTag ("Player") as GameObject;
			target = _player.transform;
		}
	
		CameraSetUp ();

	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (1))
			_camButtonDown = true;
		if (Input.GetMouseButtonUp (1))
			_camButtonDown = false;
	}

	void LateUpdate ()
	{
		if (target != null) {
			if (_camButtonDown) {
				_x += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
				_y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;

				_y = ClampAngle (_y, yMinLimit, yMaxLimit);

				_myTransform.rotation = Quaternion.Euler (_y, _x, 0);
				_myTransform.position = _myTransform.rotation * new Vector3 (0.0f, 0.0f, -walkDistance) + target.position;
			} else if (Input.GetAxis ("Mouse ScrollWheel") > 0)
				walkDistance -= zoomStep;
			else if (Input.GetAxis ("Mouse ScrollWheel") < 0)
				walkDistance += zoomStep;
			else {
				//_myTransform.position = new Vector3 (target.position.x, target.position.y + height, target.position.z - walkDistance);
				//_myTransform.LookAt (target);
				_x = 0;
				_y = 0;

				float wantedRotationAngle = target.eulerAngles.y;
				float wantedHeight = target.position.y + height;

				float currentRotationAngle = _myTransform.eulerAngles.y;
				float currentHeight = _myTransform.position.y;

				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

				// Damp the height
				currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

				// Convert the angle into a rotation
				Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

				// Set the position of the camera on the x-z plane
				_myTransform.position = target.position;
				_myTransform.position -= currentRotation * Vector3.forward * walkDistance;

				// Set the height of the camera
				_myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);

				// Always look at the target
				_myTransform.LookAt(target);
			}
		}

	}

	private float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;

		return Mathf.Clamp (angle, min, max);
	}

	private void CameraSetUp ()
	{
		_myTransform.position = new Vector3 (target.position.x, target.position.y + height, target.position.z - walkDistance);
		_myTransform.LookAt (target);
	}
}