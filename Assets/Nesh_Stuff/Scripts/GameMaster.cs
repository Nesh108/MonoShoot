using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public GameObject playerCharacter;
	public GameObject gameSettings;
	public Camera mainCamera;

	private const float zOffset = -2.5f;
	private const float yOffset = 2.5f;
	private const float xRotOffset = 22.5f;

	private GameObject _player;
	private PlayerCharacter _playerScript;

	// Use this for initialization
	void Start () {
		_player = Instantiate(playerCharacter, Vector3.zero, Quaternion.identity) as GameObject;
		_player.name = "Player";

		_playerScript = _player.GetComponent<PlayerCharacter>();

		mainCamera.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + yOffset, _player.transform.position.z + zOffset);
		mainCamera.transform.Rotate(xRotOffset, 0, 0);

		LoadCharacter();
	}
	
	public void LoadCharacter(){

		if(GameObject.Find("GameSettings") == null) {
			GameObject gs = Instantiate (gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs.name = "GameSettings";
		}

		GameObject.Find ("GameSettings").GetComponent<GameSettings> ().LoadCharacterData ();

	}
}
