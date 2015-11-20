using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(this);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SaveCharacterData() {
		GameObject player = GameObject.Find("Player");
		PlayerCharacter playerClass = player.GetComponent<PlayerCharacter>();

		PlayerPrefs.DeleteAll();

		PlayerPrefs.SetString("Player_Name", playerClass.Name);

		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			PlayerPrefs.SetInt(((AttributeName)i).ToString () + " - Base Value", playerClass.GetPrimaryAttribute (i).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)i).ToString () + " - Exp To Level", playerClass.GetPrimaryAttribute (i).ExpToLevel);
		}

		for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			PlayerPrefs.SetInt(((VitalName)i).ToString () + " - Base Value", playerClass.GetVital (i).BaseValue);
			PlayerPrefs.SetInt(((VitalName)i).ToString () + " - Exp To Level", playerClass.GetVital (i).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)i).ToString () + " - Cur Value", playerClass.GetVital (i).CurValue);
			

			PlayerPrefs.SetString(((VitalName)i).ToString () + " - Mods", playerClass.GetVital(i).ToString());

		}

		for (int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
			PlayerPrefs.SetInt(((SkillName)i).ToString () + " - Base Value", playerClass.GetSkill (i).BaseValue);
			PlayerPrefs.SetInt(((SkillName)i).ToString () + " - Exp To Level", playerClass.GetSkill (i).ExpToLevel);

			PlayerPrefs.SetString(((SkillName)i).ToString () + " - Mods", playerClass.GetSkill(i).ToString());
		}
	}

	public void LoadCharacterData() {
		GameObject player = GameObject.Find("Player");
		PlayerCharacter playerClass = player.GetComponent<PlayerCharacter>();

		playerClass.Name = PlayerPrefs.GetString("Player_Name", "Name Me");

		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			playerClass.GetPrimaryAttribute (i).BaseValue = PlayerPrefs.GetInt(((AttributeName)i).ToString () + " - Base Value", 0);
			playerClass.GetPrimaryAttribute (i).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)i).ToString () + " - Exp To Level", 0);

			Debug.Log (playerClass.GetPrimaryAttribute (i).BaseValue + " : " + playerClass.GetPrimaryAttribute (i).ExpToLevel);
		}

		for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			playerClass.GetVital (i).BaseValue = PlayerPrefs.GetInt(((VitalName)i).ToString () + " - Base Value", 0);
			playerClass.GetVital (i).ExpToLevel = PlayerPrefs.GetInt(((VitalName)i).ToString () + " - Exp To Level", 0);
			playerClass.GetVital (i).CurValue = PlayerPrefs.GetInt(((VitalName)i).ToString () + " - Cur Value", 0);
			
			
			//playerClass.GetVital(i).ToString() = PlayerPrefs.GetString(((VitalName)i).ToString () + " - Mods", "");
			
		}
	}
}
