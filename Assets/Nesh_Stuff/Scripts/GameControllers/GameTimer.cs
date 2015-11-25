using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
	public enum TimeOfDay
	{
		Idle,
		SunRise,
		SunSet
	}

	public Transform[] suns;
	public float dayCycleInMinutes = 1f;
	public float sunRise;					// Time of day for sunrise
	public float sunSet;					// Time of day for sunset
	public float skyboxBlendModifier;		// The speed at which the textures in the skyboxes blend

	private Sun[] _sunScript;
	private const float SECOND = 1f;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;
	private float _degreeRotation;
	private float _timeOfDay;
	private float _dayCycleInSeconds;
	private int _daysPassed = 0;
	private TimeOfDay _tod;

	// Use this for initialization
	void Start ()
	{

		_tod = TimeOfDay.Idle;

		_dayCycleInSeconds = dayCycleInMinutes * MINUTE;

		RenderSettings.skybox.SetFloat ("_Blend", 0);

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
		_degreeRotation = DEGREES_PER_SECOND * DAY / _dayCycleInSeconds;

		// Convert values to seconds
		sunRise *= _dayCycleInSeconds; 
		sunSet *= _dayCycleInSeconds; 
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < suns.Length; i++)
			suns [i].Rotate (new Vector3 (_degreeRotation, 0, 0) * Time.deltaTime);

		_timeOfDay += Time.deltaTime;

		// Reset clock and add a day
		if (_timeOfDay > _dayCycleInSeconds) {
			_timeOfDay -= _dayCycleInSeconds;
			_daysPassed++;
		}

		if (_timeOfDay > sunRise && _timeOfDay < sunSet && RenderSettings.skybox.GetFloat ("_Blend") < 1) {
			_tod = TimeOfDay.SunRise;
			BlendSkybox ();
		} else if (_timeOfDay > sunSet && RenderSettings.skybox.GetFloat ("_Blend") > 0) {
			_tod = TimeOfDay.SunSet;
			BlendSkybox ();
		} else {
			_tod = TimeOfDay.Idle;
		}

	}

	private void BlendSkybox ()
	{
		float time = 0;

		switch (_tod) {
		case TimeOfDay.SunRise: 
			time = (_timeOfDay - sunRise) / _dayCycleInSeconds * skyboxBlendModifier;
			break;
		case TimeOfDay.SunSet:
			time = (_timeOfDay - sunSet) / _dayCycleInSeconds * skyboxBlendModifier;
			time = 1 - time;
			
			break;
		}

		RenderSettings.skybox.SetFloat ("_Blend", time);

		Debug.Log (time);
	} 

}
