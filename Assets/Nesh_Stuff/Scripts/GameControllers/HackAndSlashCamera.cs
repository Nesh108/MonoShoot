/// <summary>
/// 
/// RPG style camera
/// 
/// Attach this script to a camera
/// 
/// For this script to work properly, make sure the following axis exist in the InputManager
/// 
///     - Rotate Camera Button              - the mouse button to allow the camera rotation
///     - Mouse X                           - rotate the camera horizontally with the mouse
///     - Mouse Y                           - rotate the camera horizontally with the mouse
///     - Rotate Camera Horizontal Buttons  - keyboard buttons to rotate the camera on the x-axis
///     - Rotate Camera Vertical Buttons    - keyboard buttons to rotate the camera on the y-axis
///     - Reset Camera                      - button to reset the camera to default position
/// 
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class HackAndSlashCamera : MonoBehaviour
{
    public Transform target;
    public bool lookAtHead = false;
    public string headMeshName = "Head";
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
    public float minZoom = -0.5f;
    public float maxZoom = 5f;
    private float _x;
    private float _y;
    private Transform _myTransform;
    private GameObject _player;
    private Transform _targetHead;
    private bool _camButtonDown = false;
    private bool _rotateCameraKeyPressed = false;

    void Awake()
    {
        _myTransform = transform;
    }

    void Start()
    {
    }

    void Update()
    {
        // If the player has been loaded
        if (target != null)
        {
            if (Input.GetButtonDown("Rotate Camera Button"))
                _camButtonDown = true;
            if (Input.GetButtonUp("Rotate Camera Button"))
            {
                // Reset values
                _x = 0;
                _y = 0;
                _camButtonDown = false;
            }

            if (Input.GetButtonDown("Rotate Camera Horizontal Buttons") || Input.GetButtonDown("Rotate Camera Vertical Buttons"))
                _rotateCameraKeyPressed = true;
            if (Input.GetButtonUp("Rotate Camera Horizontal Buttons") || Input.GetButtonUp("Rotate Camera Vertical Buttons"))
            {
                // Reset values
                _x = 0;
                _y = 0;
                _rotateCameraKeyPressed = false;
            }
        }
        else if (GameObject.Find("Game Master").GetComponent<GameMaster>().isLoadComplete())
            // Setup the camera when the player/target has been loaded
            InitialSetUp();


    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (_rotateCameraKeyPressed)
            {
                _x += Input.GetAxis("Rotate Camera Horizontal Buttons") * xSpeed * 0.02f;
                _y -= Input.GetAxis("Rotate Camera Vertical Buttons") * ySpeed * 0.02f;

                RotateCamera();
            }
            else if (_camButtonDown)
            {
                _x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                RotateCamera();

            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                walkDistance -= zoomStep;
                walkDistance = Mathf.Max(minZoom, walkDistance);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                walkDistance += zoomStep;
                walkDistance = Mathf.Min(maxZoom, walkDistance);
            }
            else
            {
                //_myTransform.position = new Vector3 (target.position.x, target.position.y + height, target.position.z - walkDistance);
                //_myTransform.LookAt (target);
                Transform lookAt;

                if (lookAtHead)
                    lookAt = _targetHead;
                else
                    lookAt = target;

                float wantedRotationAngle = lookAt.eulerAngles.y;
                float wantedHeight = lookAt.position.y + height;

                float currentRotationAngle = _myTransform.eulerAngles.y;
                float currentHeight = _myTransform.position.y;

                // Damp the rotation around the y-axis
                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

                // Damp the height
                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

                // Convert the angle into a rotation
                Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                // Set the position of the camera on the x-z plane
                _myTransform.position = lookAt.position;
                _myTransform.position -= currentRotation * Vector3.forward * walkDistance;

                // Set the height of the camera
                _myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);


                // Always look at the target
                if (lookAtHead)
                    _myTransform.LookAt(_targetHead);
                else
                    _myTransform.LookAt(target);
            }
        }

    }

    private void RotateCamera()
    {
        _y = ClampAngle(_y, yMinLimit, yMaxLimit);

        _myTransform.rotation = Quaternion.Euler(_y, _x, 0);

        if (!lookAtHead)
            _myTransform.position = _myTransform.rotation * new Vector3(0.0f, 0.0f, -walkDistance) + target.position;
        else
            _myTransform.position = _myTransform.rotation * new Vector3(0.0f, 0.0f, -walkDistance) + _targetHead.position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    private void InitialSetUp()
    {
        if (target == null)
        {
            Debug.LogWarning("No target attached to camera. Trying to use player character instead, mate.");
            _player = GameObject.FindGameObjectWithTag("Player") as GameObject;

            if (_player != null)
                target = _player.transform;
            else
                Debug.LogError("Couldn't find any player either, mate.");

        }

        // Search for head in the target
        if (lookAtHead)
            foreach (Transform t in target)
                if (t.name == headMeshName)
                    _targetHead = t;

        // Head not found
        if (_targetHead == null)
            Debug.LogError("Mesh named: " + headMeshName + " not found. Did you misspell it? Or perhaps untick the look at head, mate");

        CameraSetUp();
    }

    private void CameraSetUp()
    {
        if (lookAtHead)
        {
            _myTransform.position = new Vector3(_targetHead.position.x, _targetHead.position.y + height, _targetHead.position.z - walkDistance);
            _myTransform.LookAt(_targetHead);
        }
        else
        {
            _myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
            _myTransform.LookAt(target);
        }
    }
}