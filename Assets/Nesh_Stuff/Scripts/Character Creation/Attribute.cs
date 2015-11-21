
public class Attribute : BaseStat
{
	new public const int STARTING_EXP_COST = 50;
	private string _name;

	public Attribute ()
	{
		_name = string.Empty;
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;	// 5% increase
	}

	public string Name {
		get { return _name; }
		set { _name = value; }
	}
}

public enum AttributeName
{
	Strength,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	Will,
	Charisma
}
