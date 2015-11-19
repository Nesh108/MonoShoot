using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour {

	List<Transform> targets;
	public Transform selectedTarget = null;

	private Transform myTransform;

	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		myTransform = transform;
		AddAllEnemies();
	}

	void AddAllEnemies(){
		foreach(GameObject enemy in GameObject.FindGameObjectsWithTag ("Enemy")){
			AddTarget(enemy.transform);
		}
	
	}

	void AddTarget(Transform target){
		if(selectedTarget == null)
		targets.Add (target);
	}

	private void SortTargetsByDistance(){
		targets.Sort (delegate(Transform t1, Transform t2) {
			return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
		});
	}

	private void TargetEnemy(){


		if (selectedTarget == null) {
			
			SortTargetsByDistance ();
			selectedTarget = targets [0];
		} else {
		
			int index = targets.IndexOf(selectedTarget);

			if(index  < targets.Count - 1)
				index++;
			else
				index = 0;

			DeselectTarget();
			selectedTarget = targets[index];

		}

		SelectTarget();
	}

	private void SelectTarget(){
		(selectedTarget.GetComponent ("Halo") as Behaviour).enabled = true;

		(GetComponent ("PlayerAttack") as PlayerAttack).target = selectedTarget.gameObject;
	}

	private void DeselectTarget(){
		(selectedTarget.GetComponent ("Halo") as Behaviour).enabled = false;
		selectedTarget = null;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab))
			TargetEnemy ();

	}
}
