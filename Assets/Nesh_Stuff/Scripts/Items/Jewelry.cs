using UnityEngine;

public class Jewelry : BuffItem
{
	public JewelrySlot _slot;

	public Jewelry ()
	{
		_slot = JewelrySlot.PocketItem;
	}

	public Jewelry (JewelrySlot slot)
	{
		_slot = slot;
	}

	#region - getters and setters
	public JewelrySlot Slot {
		get { return _slot; }
		set { _slot = value; }
	}
	#endregion
}

public enum JewelrySlot
{
	EarRings,
	Necklaces,
	Bracelets,
	Rings,
	PocketItem
}