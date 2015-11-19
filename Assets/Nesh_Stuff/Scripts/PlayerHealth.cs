using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth = 100;
	public int curHealth = 100;

	public float healthBarLength;
	
	// Use this for initialization
	void Start () {
		healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
		AdjustCurrentHealth (0);
	}

	void OnGUI () {
		GUI.Box (new Rect(10,10,healthBarLength,20), curHealth + "/" + maxHealth);
	}

	public void AdjustCurrentHealth(int adj){
		curHealth += adj;

		// Checks that max health is not higher than 100 or lower than 0
		curHealth = Mathf.Max (0, curHealth);
		curHealth = Mathf.Min (100, curHealth);

		// Avoid division by 0
		maxHealth = Mathf.Max (0, maxHealth);


		healthBarLength = (Screen.width / 2) * (curHealth / (float)maxHealth);
	}
}
