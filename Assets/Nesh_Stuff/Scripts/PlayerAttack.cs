using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	
	public GameObject target;
	public int weaponDamage = 2;
	public float attackDistance = 2.5f;

	private float attackTimer;
	private float coolDownMeeleAttack = 2.0f;

	// Use this for initialization
	void Start () {
		attackTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;

		attackTimer = Mathf.Max (0, attackTimer);

		if (Input.GetKeyUp (KeyCode.F) && attackTimer == 0) {
			MeeleAttack ();
			attackTimer = coolDownMeeleAttack;
		}
	}

	void MeeleAttack() {

		// Current distance from the target
		float distance = Vector3.Distance (target.transform.position, transform.position);

		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot (dir, transform.forward);

		if (distance < attackDistance && direction > 0) {
			EnemyHealth eh = (EnemyHealth)target.GetComponent ("EnemyHealth");
			eh.AdjustCurrentHealth (-weaponDamage);

			Debug.Log("Meele Attack! Enemy HP: " + eh.curHealth);
		}
	}
}
