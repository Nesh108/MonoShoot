using UnityEngine;
using System.Collections;

public class Enemy : BaseCharacter {

	public int curHealth;
	public int maxHealth;

	// Use this for initialization
	void Start () {
		GetPrimaryAttribute((int)AttributeName.Constitution).BaseValue = 100;
		GetVital((int)VitalName.Health).Update();

		Name = "EvilSmiley";
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void DisplayHealth(){
		Messenger<int, int>.Broadcast("enemy health update", curHealth, maxHealth);
	}
}
