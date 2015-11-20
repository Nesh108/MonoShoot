using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour
{

	private PlayerCharacter _char;
	private const int STARTING_POINTS = 350;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_ATTRIBUTE_VALUE = 50;
	private int pointsLeft;
	private const int OFFSET = 10;
	private const int LINE_HEIGHT = 25;
	private const int STAT_LABEL_WIDTH = 100;
	private const int BASEVALUE_LABEL_WIDTH = 30;
	private const int BUTTON_WIDTH = 25;
	private const int BUTTON_HEIGHT = 25;
	private const int STARTING_ATTR_POS = 40;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start ()
	{
		GameObject player = Instantiate (playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		player.name = "Player";

		_char = player.GetComponent<PlayerCharacter> ();
		_char.Awake ();

		pointsLeft = STARTING_POINTS;

		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			_char.GetPrimaryAttribute (i).BaseValue = STARTING_ATTRIBUTE_VALUE;
			pointsLeft -= (STARTING_ATTRIBUTE_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
		}

		_char.StatUpdate ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI ()
	{
		DisplayName ();
		DisplayPointsLeft ();
		DisplayAttributes ();
		DisplayVitals ();
		DisplaySkills ();

		DisplayCreateButton ();
	}

	private void DisplayName ()
	{
		GUI.Label (new Rect (OFFSET, OFFSET, STAT_LABEL_WIDTH, LINE_HEIGHT), "Name:");
		_char.Name = GUI.TextField (new Rect (85, 10, 130, LINE_HEIGHT), _char.Name);
	}

	private void DisplayAttributes ()
	{
		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			GUI.Label (new Rect (OFFSET, STARTING_ATTR_POS + (i * BASEVALUE_LABEL_WIDTH), STAT_LABEL_WIDTH, LINE_HEIGHT), ((AttributeName)i).ToString ());
			GUI.Label (new Rect (STAT_LABEL_WIDTH + OFFSET, STARTING_ATTR_POS + (i * BASEVALUE_LABEL_WIDTH), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _char.GetPrimaryAttribute (i).AdjustedBaseValue.ToString ());

			if (GUI.Button (new Rect (150, STARTING_ATTR_POS + (i * BASEVALUE_LABEL_WIDTH), BUTTON_WIDTH, BUTTON_HEIGHT), "-")) {
				if (_char.GetPrimaryAttribute (i).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
					_char.GetPrimaryAttribute (i).BaseValue--;
					pointsLeft++;
					_char.StatUpdate ();
				}
			}

			if (GUI.Button (new Rect (180, STARTING_ATTR_POS + (i * BASEVALUE_LABEL_WIDTH), BUTTON_WIDTH, BUTTON_HEIGHT), "+")) {
				if (pointsLeft > 0) {
					_char.GetPrimaryAttribute (i).BaseValue++;
					pointsLeft--;
					_char.StatUpdate ();
				}
			}
		}
	}

	private void DisplayVitals ()
	{
		for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			GUI.Label (new Rect (OFFSET, STARTING_ATTR_POS + ((i + Enum.GetValues (typeof(AttributeName)).Length) * BASEVALUE_LABEL_WIDTH), STAT_LABEL_WIDTH, LINE_HEIGHT), ((VitalName)i).ToString ());
			GUI.Label (new Rect (STAT_LABEL_WIDTH + OFFSET, STARTING_ATTR_POS + ((i + Enum.GetValues (typeof(AttributeName)).Length) * BASEVALUE_LABEL_WIDTH), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _char.GetVital (i).AdjustedBaseValue.ToString ());
		}
	}

	private void DisplaySkills ()
	{
		for (int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
			GUI.Label (new Rect (250, STARTING_ATTR_POS + (i * BASEVALUE_LABEL_WIDTH), STAT_LABEL_WIDTH, LINE_HEIGHT), ((SkillName)i).ToString ());
			GUI.Label (new Rect (355, STARTING_ATTR_POS + (i * BASEVALUE_LABEL_WIDTH), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), _char.GetSkill (i).AdjustedBaseValue.ToString ());
		}
	}

	private void DisplayPointsLeft ()
	{
		GUI.Label (new Rect (250, 10, STAT_LABEL_WIDTH, LINE_HEIGHT), "Points Left: " + pointsLeft);
	}
	
	private void DisplayCreateLabel ()
	{
		GUI.Label (new Rect (Screen.width / 2 - 50, STARTING_ATTR_POS + ((Enum.GetValues (typeof(AttributeName)).Length + Enum.GetValues (typeof(VitalName)).Length) * BASEVALUE_LABEL_WIDTH), STAT_LABEL_WIDTH, LINE_HEIGHT), "Create", "Button");
	}

	private void DisplayCreateButton ()
	{

		if(_char.Name == "" || pointsLeft > 0)
			GUI.enabled = false;

		if (GUI.Button (new Rect (Screen.width / 2 - 50, STARTING_ATTR_POS + ((Enum.GetValues (typeof(AttributeName)).Length + Enum.GetValues (typeof(VitalName)).Length) * BASEVALUE_LABEL_WIDTH), STAT_LABEL_WIDTH, LINE_HEIGHT), "Create")) {

			// Change cur value of vital to max modified value of that vital
			UpdateCurVitalValues();

			GameObject.Find ("GameSettings").GetComponent<GameSettings> ().SaveCharacterData ();

			Application.LoadLevel (1);
		}

		GUI.enabled = true;
	}

	private void UpdateCurVitalValues(){
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++)
			_char.GetVital(i).CurValue = _char.GetVital(i).AdjustedBaseValue;
	}
}
