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

		PlayerPrefs.SetString("Player_Name", playerClass.Name);

		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			PlayerPrefs.SetInt(((AttributeName)i).ToString () + " - Base Value", playerClass.GetPrimaryAttribute (i).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)i).ToString () + " - Exp To Level", playerClass.GetPrimaryAttribute (i).ExpToLevel);
		}

		for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			PlayerPrefs.SetInt(((VitalName)i).ToString () + " - Base Value", playerClass.GetVital (i).BaseValue);
			PlayerPrefs.SetInt(((VitalName)i).ToString () + " - Exp To Level", playerClass.GetVital (i).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)i).ToString () + " - Cur Value", playerClass.GetVital (i).CurValue);
		
			playerClass.GetVital(i).ToString();
		}
	}

	public void LoadCharacterData() {
		
	}
}
