using UnityEngine;
using System.Collections;

abstract public class Item
{
#region Variables (protected)
	
	protected enum ItemType
	{
		Default,
		Weapon,
		Armor
	}

	protected ItemType m_eItemType = ItemType.Default;

	protected Character m_pUser = null;
	
	#endregion
	
#region Variables (private)
	
	
	
	#endregion


	public void Initialize(Character pUser)
	{
		m_pUser = pUser;
	}
}
