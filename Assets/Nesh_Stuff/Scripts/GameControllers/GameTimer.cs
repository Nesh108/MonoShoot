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
	public float morningLight;				// At what time the morning effects should start
	public float nightLight;				// At what time the night effects should start

	private bool _isMorning = false;
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
	private float _noonTime;				// Time of the day at which it is noon

	private float _morningLength;			// Time from sunrise to noon
	private float _eveningLength;			// Time from noon to sunset

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
		_noonTime = _dayCycleInSeconds / 2;
		_morningLength = _noonTime - sunRise;		// Length of the morning in secs
		_eveningLength = sunSet - _noonTime;		// Length of the evening in secs
		morningLight *= _dayCycleInSeconds;
		nightLight *= _dayCycleInSeconds;

		// Setup intensity lights to min start value
		SetupLighting ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < suns.Length; i++)
			suns [i].Rotate (new Vector3 (_degreeRotation, 0, 0) * Time.deltaTime);

		_timeOfDay += Time.deltaTime;

		// Control ligthing effects according to the time of day
		if(!_isMorning && _timeOfDay > morningLight && _timeOfDay < nightLight){
			_isMorning = true;
			Messenger<bool>.Broadcast ("Morning Light Time", true);
			Debug.Log ("It's morning, mate!");
		}
		else if(_isMorning && _timeOfDay > nightLight){
			_isMorning = false;
			Messenger<bool>.Broadcast ("Morning Light Time", false);
			Debug.Log("It's night, mate!");
		}

		// Reset clock and add a day
		if (_timeOfDay > _dayCycleInSeconds) {
			_timeOfDay -= _dayCycleInSeconds;
			_daysPassed++;

			Debug.Log ("Day: " + _daysPassed + " - Rise and shine. A new day is here, mate!");
		}

		if (_timeOfDay > sunRise && _timeOfDay < _noonTime)
			AdjustLighting (true);
		else if (_timeOfDay > _noonTime && _timeOfDay < sunSet)
			AdjustLighting (false);

		// Check the time of the day and act accordingly
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

		//Debug.Log (time);
	}

	private void SetupLighting ()
	{
		for (int i = 0; i < _sunScript.Length; i++)
			if (_sunScript [i].givesLight)
				suns [i].GetComponent<Light> ().intensity = _sunScript [i].minLightBrightness;
	}

	private void AdjustLighting (bool brighten)
	{
		if (brighten) {
			float pos = (_timeOfDay - sunRise) / _morningLength;		// current position of the sun in the sky
			foreach (Sun s in _sunScript)
				if (s.givesLight)
					s.GetComponent<Light> ().intensity = s.maxLightBrightness * pos;
		} else {
			float pos = (sunSet - _timeOfDay) / _eveningLength;		// current position of the sun in the sky
			foreach (Sun s in _sunScript)
				if (s.givesLight && (s.maxLightBrightness * pos) >= s.minLightBrightness)
					s.GetComponent<Light> ().intensity = s.maxLightBrightness * pos;
		}
	}


}
