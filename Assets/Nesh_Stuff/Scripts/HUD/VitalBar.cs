/// <summary>
/// Vital bar.
/// 
/// Displays health bar for the player or the enemy
/// </summary>
using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour
{

	public bool _isPlayerHealthBar;
	private int _maxBarLength;
	private int _curBarLength;
	private RectTransform _display;
	public GameObject healthBar;

	// Use this for initialization
	void Start ()
	{
	
		_display = healthBar.GetComponent<RectTransform> ();
		_maxBarLength = (int)_display.rect.width;

		OnEnable ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void OnEnable ()
	{
		if (_isPlayerHealthBar)
			Messenger<int, int>.AddListener ("player health update", OnChangeHealthBarSize);
		else {
			ToggleDisplay (false);
			Messenger<int, int>.AddListener ("enemy health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener ("show enemy vitalbars", ToggleDisplay);
		}
	}

	public void OnDisable ()
	{
		if (_isPlayerHealthBar)
			Messenger<int, int>.RemoveListener ("player health update", OnChangeHealthBarSize);
		else {
			ToggleDisplay (false);
			Messenger<int, int>.RemoveListener ("enemy health update", OnChangeHealthBarSize);
			Messenger<bool>.RemoveListener ("show enemy vitalbars", ToggleDisplay);
		}
	}

	public void OnChangeHealthBarSize (int curHealth, int maxHealth)
	{
		_curBarLength = (int)((curHealth / (float)maxHealth) * _maxBarLength);
		_display.sizeDelta = new Vector2 (_curBarLength, _display.rect.height);
	}

	public void SetPlayerHealthBar (bool b)
	{
		_isPlayerHealthBar = b;
	}

	private void ToggleDisplay (bool show)
	{
		healthBar.SetActive (show);
	}


}
