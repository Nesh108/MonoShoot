using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	
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

		if (attackTimer == 0) {
			MeeleAttack ();
			attackTimer = coolDownMeeleAttack;
		}
	}

	private void MeeleAttack() {

		// Current distance from the target
		float distance = Vector3.Distance (target.transform.position, transform.position);

		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot (dir, transform.forward);

		if (distance < attackDistance && direction > 0) {
			PlayerHealth eh = (PlayerHealth)target.GetComponent ("PlayerHealth");
			eh.AdjustCurrentHealth (-weaponDamage);

			Messenger<int, int>.Broadcast("player health update", eh.curHealth, eh.maxHealth);
			Debug.Log ("Enemy ATTACKED: " + eh.curHealth + "hp");
		}

	}
}
