using UnityEngine;

public class Item {
	private string _name;
	private int _value;
	private RarityTypes _rarity;
	private int _curDurability;
	private int _maxDurability;

	public Item(){
		_name = "Need Name";
		_value = 0;
		_rarity = RarityTypes.Common;
		_maxDurability = 50;
		_curDurability = _maxDurability;
	}

	public Item(string name, int value, RarityTypes rarity, int maxDur, int curDur){
		_name = name;
		_value = value;
		_rarity = rarity;
		_maxDurability = maxDur;
		_curDurability = curDur;
	}

	#region - getters and setters
	private string Name {
		get { return _name; }
		set { _name = value; }
	}

	private int Value {
		get { return _value; }
		set { _value = value; }
	}

	private RarityTypes Rarity {
		get { return _rarity; }
		set { _rarity = value; }
	}

	private int MaxDurability {
		get { return _maxDurability; }
		set { _maxDurability = value; }
	}

	private int CurDurability {
		get { return _curDurability; }
		set { _curDurability = value; }
	}

	#endregion

}

public enum RarityTypes {
	Common,
	Uncommon,
	Rare,
	Epic,
	Ultra
}