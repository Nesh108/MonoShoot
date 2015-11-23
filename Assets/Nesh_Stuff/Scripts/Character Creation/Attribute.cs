
public class Attribute : BaseStat
{
	new public const int STARTING_EXP_COST = 50;

	public Attribute ()
	{
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;	// 5% increase
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
