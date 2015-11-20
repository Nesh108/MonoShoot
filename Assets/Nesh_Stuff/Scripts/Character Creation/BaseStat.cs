﻿
public class BaseStat
{
	private int _baseValue;			// base value of the stat
	private int _buffValue;			// amount of the buff of the stat
	private int _expToLevel;		// exp required to next level
	private float _levelModifier;	// modifier applied to the skill

	public BaseStat ()
	{
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;	// 10% more per level
		_expToLevel = 100;
	}

#region Setters and Getters
	public int BaseValue {
		get{ return _baseValue; }
		set{ _baseValue = value; }
	}

	public int BuffValue {
		get{ return _buffValue; }
		set{ _buffValue = value; }
	}

	public int ExpToLevel {
		get{ return _expToLevel; }
		set{ _expToLevel = value; }
	}

	public float LevelModifier {
		get{ return _levelModifier; }
		set{ _levelModifier = value; }
	}
#endregion

	private float CalculateExpToLevel ()
	{
		return _expToLevel * _levelModifier;
	}

	public void LevelUp ()
	{
		_expToLevel = (int)CalculateExpToLevel ();
		_baseValue++;
	}

	public int AdjustedBaseValue {
		get { return _baseValue + _buffValue; }
	}


}
