using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{

	public GameObject playerCharacter;
	public GameObject gameSettings;
	//public Camera mainCamera;
	private const float zOffset = -2.5f;
	private const float yOffset = 2.5f;
	private const float xRotOffset = 22.5f;
	private GameObject _player;
	private PlayerCharacter _playerScript;
	private Vector3 _playerSpawnPointPos;		// Spawn point in 3D space for player

	// Use this for initialization
	void Start ()
	{
		_playerSpawnPointPos = new Vector3 (0.0f, 0.057f, -136.9f);
		GameObject go = GameObject.Find (GameSettings.PLAYER_SPAWN_POINT) as GameObject;

		if (go == null) {
			Debug.LogWarning ("Couldn't find Player Spawn Point. Did you forget to add it, mate?");

			go = new GameObject (GameSettings.PLAYER_SPAWN_POINT);
			Debug.Log ("Created Player Spawn Point");

			go.transform.position = _playerSpawnPointPos;
			Debug.Log ("Moved Player Spawn Point");
		}
		_player = Instantiate (playerCharacter, go.transform.position, Quaternion.identity) as GameObject;
		_player.name = "Player";

		_playerScript = _player.GetComponent<PlayerCharacter> ();

//		mainCamera.transform.position = new Vector3 (_player.transform.position.x, _player.transform.position.y + yOffset, _player.transform.position.z + zOffset);
//		mainCamera.transform.Rotate (xRotOffset, 0, 0);

		LoadCharacter ();
	}
	
	public void LoadCharacter ()
	{

		if (GameObject.Find ("GameSettings") == null) {
			GameObject gs = Instantiate (gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs.name = "GameSettings";
		}

		GameObject.Find ("GameSettings").GetComponent<GameSettings> ().LoadCharacterData ();

	}
}
