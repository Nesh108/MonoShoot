using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour
{

	List<Transform> targets;
	public Transform selectedTarget = null;
	private Transform myTransform;

	// Use this for initialization
	void Start ()
	{
		targets = new List<Transform> ();
		myTransform = transform;
		AddAllEnemies ();
	}

	void AddAllEnemies ()
	{
		foreach (GameObject enemy in GameObject.FindGameObjectsWithTag ("Enemy")) {
			AddTarget (enemy.transform);
		}
	
	}

	void AddTarget (Transform target)
	{
		if (selectedTarget == null)
			targets.Add (target);

	}

	private void SortTargetsByDistance ()
	{
		targets.Sort (delegate(Transform t1, Transform t2) {
			return Vector3.Distance (t1.position, myTransform.position).CompareTo (Vector3.Distance (t2.position, myTransform.position));
		});
	}

	private void TargetEnemy ()
	{
		if (targets.Count == 0)
			AddAllEnemies ();
	
		if (selectedTarget == null) {
			
			SortTargetsByDistance ();
			selectedTarget = targets [0];
		} else {
		
			int index = targets.IndexOf (selectedTarget);

			if (index < targets.Count - 1)
				index++;
			else
				index = 0;

			DeselectTarget ();
			selectedTarget = targets [index];

		}

		SelectTarget ();

	}

	private void SelectTarget ()
	{
		// Activate Selection Halo
		(selectedTarget.GetComponent ("Halo") as Behaviour).enabled = true;

		// Show Target name
		Transform name = selectedTarget.FindChild ("Name");
		
		if (name == null) {
			Debug.LogError ("Could not find the Name on " + selectedTarget.name + ", mate.");
			return;
		}

		name.GetComponent<TextMesh> ().text = selectedTarget.GetComponent<Enemy> ().Name;
		selectedTarget.FindChild ("Name").GetComponent<MeshRenderer> ().enabled = true;

		// Update the health bar updated according to the target

		selectedTarget.GetComponent<Enemy> ().DisplayHealth ();

		
		Messenger<bool>.Broadcast ("show enemy vitalbars", true);

	}

	private void DeselectTarget ()
	{
		// Deactivate Selection Halo
		(selectedTarget.GetComponent ("Halo") as Behaviour).enabled = false;

		// Hide Target name
		selectedTarget.FindChild ("Name").GetComponent<MeshRenderer> ().enabled = false;

		selectedTarget = null;

		Messenger<bool>.Broadcast ("show enemy vitalbars", false);

	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Tab))
			TargetEnemy ();

	}
}
