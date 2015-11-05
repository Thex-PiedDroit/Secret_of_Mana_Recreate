using UnityEngine;
using System.Collections;

public class Armor : Item
{
	#region Variables (public)
	
	public enum ArmorType
	{
		Default,
		Light,
		Medium,
		Heavy
	}
	
	#endregion
	
	#region Variables (private)

	private ArmorType m_eArmorType = ArmorType.Default;
	
	#endregion


	public Armor(Character pUser, ArmorType eArmorType) : base(pUser, ItemType.Armor)
	{
		m_eArmorType = eArmorType;
	}
}
