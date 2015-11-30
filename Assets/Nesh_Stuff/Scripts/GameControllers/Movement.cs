using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float rotateSpeed = 150.0f;
    private Transform _myTransform;

    void Awake()
    {
        _myTransform = transform;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxis("Rotate Player")) > 0)
            _myTransform.Rotate(0, Input.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed, 0);
	}
}
