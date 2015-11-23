
public class Weapon : BuffItem
{
	private int _maxDamage;
	private float _damageVariance;
	private float _maxRange;
	private DamageType _dmgType;

	public Weapon ()
	{
		_maxDamage = 0;
		_damageVariance = 0f;
		_maxRange = 0f;
		_dmgType = DamageType.Bludgeon;
	}

	public Weapon(int maxDmg, float dmgVar, float maxRange, DamageType dmgType){
		_maxDamage = maxDmg;
		_damageVariance = dmgVar;
		_maxRange = maxRange;
		_dmgType = dmgType;
	}

	#region
	public int MaxDamage {
		get { return _maxDamage; }
		set { _maxDamage = value; }
	}

	public float DamageVariance {
		get { return _damageVariance; }
		set { _damageVariance = value; }
	}

	public float MaxRange {
		get { return _maxRange; }
		set { _maxRange = value; }
	}

	public DamageType TypeOfDamage {
		get { return _dmgType; }
		set { _dmgType = value; }
	}
	#endregion
}

public enum DamageType {
	Bludgeon,
	Pierce,
	Slash,
	Fire,
	Ice,
	Earth,
	Lighting,
	Acid,
	Spirit,
	Ethereal
}
