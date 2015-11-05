﻿using UnityEngine;
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
	
#region Variables (private)
	
	
	
	#endregion


	public Item(Character pUser, ItemType eItemType)
	{
		m_pUser = pUser;
		m_eItemType = eItemType;
	}
}
