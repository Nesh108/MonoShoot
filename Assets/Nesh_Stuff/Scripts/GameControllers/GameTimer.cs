using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
	public Transform[] suns;
	private Sun[] _sunScript;
	public float dayCycleInMinutes = 1f;
	private const float SECOND = 1f;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;
	private float _degreeRotation;
	private float _timeOfDay;

	// Use this for initialization
	void Start ()
	{
		_sunScript = new Sun[suns.Length];

		for (int i = 0; i < suns.Length; i++) {
			Sun tempSunScript = suns [i].GetComponent<Sun> ();

			if (tempSunScript == null) {
				Debug.LogWarning ("Sun script not found! Adding it myself, mate.");
				suns [i].gameObject.AddComponent<Sun> ();

				tempSunScript = suns [i].GetComponent<Sun> ();
			}

			_sunScript [i] = tempSunScript;
		}
		_timeOfDay = 0;
		_degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes * MINUTE);
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < suns.Length; i++)
			suns [i].Rotate (new Vector3 (_degreeRotation, 0, 0) * Time.deltaTime);

		_timeOfDay += Time.deltaTime;
		Debug.Log (_timeOfDay);
	}
}
