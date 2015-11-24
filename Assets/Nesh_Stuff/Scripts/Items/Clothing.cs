using UnityEngine;

public class Clothing : BuffItem
{
	private ArmorSlot _slot;

	public Clothing ()
	{
		_slot = ArmorSlot.Head;
	}

	public Clothing (ArmorSlot slot)
	{
		_slot = slot;
	}

	#region - getters and setters
	public ArmorSlot Slot {
		get { return _slot; }
		set { _slot = value; }
	}
	#endregion
}

public enum ArmorSlot
{
	Head,
	Shoulders,
	UpperBody,
	Torso,
	Legs,
	Hands,
	Feet,
	Back
}