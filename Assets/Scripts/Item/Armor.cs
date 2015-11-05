using UnityEngine;
using System.Collections;

public class Armor : Item
{
	public enum ArmorType
	{
		Default,
		Light,
		Medium,
		Heavy
	}
	
#region Variables (private)

	private ArmorType m_eArmorType = ArmorType.Default;

	private int m_iDef = 0;
	
	#endregion


	public Armor(Character pUser, ArmorType eArmorType) : base(pUser, ItemType.Armor)
	{
		m_eArmorType = eArmorType;

		switch(m_eArmorType)
		{
		case ArmorType.Light:
			m_iDef = 2;
			break;
		case ArmorType.Medium:
			m_iDef = 4;
			break;
		case ArmorType.Heavy:
			m_iDef = 6;
			break;
		}
	}


	public int Def
	{
		get { return m_iDef; }
	}
}
