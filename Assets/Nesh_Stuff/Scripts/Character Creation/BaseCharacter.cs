using UnityEngine;
using System.Collections;
using System;

public class BaseCharacter : MonoBehaviour {

	private string _name;
	private int _level;
	private uint _unusedExp;

	private Attribute[] _primaryAttributes;
	private Vital[] _vitals;
	private Skill[] _skills;

	void Awake() {
		_name = string.Empty;
		_level = 0;
		_unusedExp = 0;
		
		_primaryAttributes = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vitals = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skills = new Skill[Enum.GetValues(typeof(SkillName)).Length];
	
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}

#region Setters and Getters
	public String Name {
		get { return _name; }
		set { _name = value; }
	}

	public int Level {
		get { return _level; }
		set { _level = value; }
	}

	public uint UnusedExp {
		get { return _unusedExp; }
		set { _unusedExp = value; }
	}

	public void AddExp(uint exp) {
		_unusedExp += exp;
		CalculateLevel();
	}
	
	public Attribute GetPrimaryAttribute(int index){
		return _primaryAttributes[index];
	}

	public Vital GetVital(int index){
		return _vitals[index];
	}

	public Skill GetSkill(int index){
		return _skills[index];
	}
#endregion

	// TODO: Improve this
	public void CalculateLevel() {

	}

	private void SetupPrimaryAttributes(){
		for(int i = 0; i < _primaryAttributes.Length; i++)
			_primaryAttributes[i] = new Attribute();
	}

	private void SetupVitals(){
		for(int i = 0; i < _vitals.Length; i++)
			_vitals[i] = new Vital();
	}

	private void SetupSkills(){
		for(int i = 0; i < _skills.Length; i++)
			_skills[i] = new Skill();
	}

	private void SetupVitalModifiers(){
		// Health -  Half of the constitution to the Health
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), .5f));
	
		// Energy -  Whole the Constitution to the Health
		GetVital((int)VitalName.Energy).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), 1f));

		// Mana -  Whole the Will to the Mana
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Will), 1f));
	}


	private void SetupSkillModifiers(){
		// Melee Offense 
		GetSkill((int)SkillName.Melee_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .33f));
		GetSkill((int)SkillName.Melee_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Nimbleness), .33f));

		// Melee Defense 
		GetSkill((int)SkillName.Melee_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		GetSkill((int)SkillName.Melee_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), .33f));

		// Magic Offense 
		GetSkill((int)SkillName.Magic_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Magic_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Will), .33f));

		// Magic Defense 
		GetSkill((int)SkillName.Magic_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Magic_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Will), .33f));

		// Ranged Offense 
		GetSkill((int)SkillName.Ranged_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Ranged_Offense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));

		// Ranged Defense 
		GetSkill((int)SkillName.Ranged_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		GetSkill((int)SkillName.Ranged_Defense).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Nimbleness), .33f));

	}

	public void StatUpdate(){
		for(int i = 0; i < _vitals.Length; i++)
			_vitals[i].Update();

		for(int i = 0; i < _skills.Length; i++)
			_skills[i].Update();
	}
}
