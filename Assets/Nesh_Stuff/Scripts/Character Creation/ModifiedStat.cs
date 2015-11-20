using System.Collections.Generic;

public class ModifiedStat : BaseStat
{

	private List<ModifyingAttribute> _mods;		// List of attributes that modify the stat
	private int _modValue;						// Amount added to the base value of the modifiers

	public ModifiedStat ()
	{
		_mods = new List<ModifyingAttribute> ();
		_modValue = 0;
	}

	public void AddModifier (ModifyingAttribute mod)
	{
		_mods.Add (mod);
	}

	private void CalculateModValue ()
	{
		_modValue = 0;

		foreach (ModifyingAttribute attr in _mods)
			_modValue += (int)(attr.attribute.AdjustedBaseValue * attr.ratio);
	}

	public new int AdjustedBaseValue {
		get { return BaseValue + BuffValue + _modValue; }
	}

	public void Update ()
	{
		CalculateModValue ();
	}

	public string ToString ()
	{

		string temp;

		for (int i = 0; i < _mods.Count; i++)
			temp += _mods [i].attribute.Name + "_" + _mods [i].ratio;
	

		return temp;
	}
}

public struct ModifyingAttribute
{
	public Attribute attribute;
	public float ratio;

	public ModifyingAttribute (Attribute attr, float r)
	{
		attribute = attr;
		ratio = r;
	}
}


