
public class Attribute : BaseStat {

	public Attribute() {
		ExpToLevel = 50;
		LevelModifier = 1.05f;	// 5% increase
	}

}

public enum AttributeName {
	Strength,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	Will,
	Charisma
}
