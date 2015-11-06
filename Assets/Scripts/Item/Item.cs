using UnityEngine;
using System.Collections;

abstract public class Item
{
	public enum ItemType
	{
		Default,
		Weapon,
		Armor
	}

#region Variables (protected)
	
	protected ItemType m_eItemType = ItemType.Default;

	protected Character m_pUser = null;
	
	#endregion


	static public void S_Initialize()
	{
		Sword.S_SwordInitialize();
		Bow.S_BowInitialize();
		Staff.S_StaffInitialize();
		Armor.S_ArmorInitialize();
	}


	public Item(Character pUser, ItemType eItemType)
	{
		m_pUser = pUser;
		m_eItemType = eItemType;
	}


	public ItemType ItmType
	{
		get { return m_eItemType; }
	}

	virtual public GameObject IconRef
	{
		get { return null; }
	}
}
