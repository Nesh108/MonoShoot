using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour
{

	public enum State
	{
		Idle,
		Initalize,
		Setup,
		SpawnEnemy
	}

	public GameObject[] enemyPrefabs;		// array of all the enemy to be spawned
	public GameObject[] spawnPoints;		// array of all the spawn points

	public State state;

	void Awake ()
	{
		state = EnemyGenerator.State.Initalize;
	}

	// Use this for initialization
	IEnumerator Start ()
	{
		while (true) {
			switch (state) {
			case State.Initalize: 
				Initialize ();
				break;
			case State.Setup: 
				Setup ();
				break;
			case State.SpawnEnemy:
				SpawnEnemy ();
				break;
			}

			yield return 0;
		}
	}
	
	private void Initialize ()
	{
		Debug.Log ("Init function");

		if (!CheckEnemyPrefabs () || !CheckSpawnPoints ())
			return;

		state = EnemyGenerator.State.Setup;
	}
	
	private void Setup ()
	{
		Debug.Log ("Setup function");

		state = EnemyGenerator.State.SpawnEnemy;
	}
	
	private void SpawnEnemy ()
	{
		Debug.Log ("SpawnEnemy function");

		foreach (GameObject spawnPt in EmptySpawnPoints ()) {
			GameObject enemy = Instantiate (enemyPrefabs [Random.Range (0, enemyPrefabs.Length)], spawnPt.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = spawnPt.transform;
			enemy.transform.parent.transform.Rotate (new Vector3 (0, 180, 0));
		}

		state = EnemyGenerator.State.Idle;
	}

	private bool CheckEnemyPrefabs ()
	{
		return enemyPrefabs.Length > 0 ? true : false;
	}

	private bool CheckSpawnPoints ()
	{
		return spawnPoints.Length > 0 ? true : false;
	}

	// Generate list of available spawn points without enemies as child
	private GameObject[] EmptySpawnPoints ()
	{
		List<GameObject> gos = new List<GameObject> ();

		foreach (GameObject spawnPt in spawnPoints) {
			if (spawnPt.transform.childCount == 0)
				gos.Add (spawnPt);
		}

		return gos.ToArray ();
	}

}
