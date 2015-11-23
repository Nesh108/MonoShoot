/// <summary>
/// Base stat.
/// 
/// This is the base class for a stat in game
/// </summary>
public class BaseStat
{
	public const int STARTING_EXP_COST = 100;
	private int _baseValue;			// base value of the stat
	private int _buffValue;			// amount of the buff of the stat
	private int _expToLevel;		// exp required to next level
	private float _levelModifier;	// modifier applied to the skill

	private string _name;

	public BaseStat ()
	{		
		_name = string.Empty;
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;	// 10% more per level
		_expToLevel = STARTING_EXP_COST;
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
	
	public string Name {
		get { return _name; }
		set { _name = value; }
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
