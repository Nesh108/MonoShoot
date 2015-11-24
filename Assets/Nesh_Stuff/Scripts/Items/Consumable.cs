using UnityEngine;

public class Consumable : BuffItem
{

	private Vital[] _vitals;		// List of vitals healed by this consumable
	private int[] _amountHealing;	// Amount to heal for each vital

	private float _buffTime;		// How long the buff will last

	public Consumable ()
	{
		Reset ();
	}

	public Consumable (Vital[] vs, int[] ah, float buffTime)
	{
		_vitals = vs;
		_amountHealing = ah;
		_buffTime = buffTime;
	}

	// Each given vital is created and set to 0
	void Reset ()
	{
		_buffTime = 0f;

		for (int i = 0; i < _vitals.Length; i++) {
			_vitals [i] = new Vital ();
			_amountHealing [i] = 0;
		}
	}

	public int VitalCount ()
	{
		return _vitals.Length;
	}

	public Vital GetVitalAt (int index)
	{
		if (index < _vitals.Length && index > -1)
			return _vitals [index];

		return new Vital ();
	}

	public int HealingAt (int index)
	{
		if (index < _amountHealing.Length && index > -1)
			return _amountHealing [index];
		
		return 0;
	}
	
	public void SetVitalAt (int index, Vital v)
	{
		if (index < _amountHealing.Length && index > -1)
			_vitals [index] = v;
	}

	public void SetHealingAt (int index, int h)
	{
		if (index < _amountHealing.Length && index > -1)
			_amountHealing [index] = h;
	}

	public void SetVitalAndHealingAt (int index, Vital v, int h)
	{
		SetVitalAt (index, v);
		SetHealingAt (index, h);
	}
	
	#region - getters and setters
	public float BuffTime {
		get { return _buffTime; }
		set { _buffTime = value; }
	}
	#endregion
}
