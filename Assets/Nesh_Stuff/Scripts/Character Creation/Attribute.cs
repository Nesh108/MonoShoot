
public class Attribute : BaseStat
{
	private string _name;

	public Attribute ()
	{
		_name = string.Empty;
		ExpToLevel = 50;
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
