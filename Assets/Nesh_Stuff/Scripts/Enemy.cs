using UnityEngine;
using System.Collections;

public class Enemy : BaseCharacter {

	public int curHealth;
	public int maxHealth;

	public string name;

	// Use this for initialization
	void Start () {
		GetPrimaryAttribute((int)AttributeName.Constitution).BaseValue = 100;
		GetVital((int)VitalName.Health).Update();

		if(name == null)
			Name = "A Mob With No Name";
		else
			Name = name;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void DisplayHealth(){
		Messenger<int, int>.Broadcast("enemy health update", curHealth, maxHealth);
	}
}
