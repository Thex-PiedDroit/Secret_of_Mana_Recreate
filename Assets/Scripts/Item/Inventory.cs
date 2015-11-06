using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{
	#region Variables (private)

	private List<Item> pItems = new List<Item>();
	private Weapon pEquipedWeapon = null;
	private Armor pEquipedArmor = null;
	
	#endregion


	public void AddItem(Item pItem)
	{
		pItems.Add(pItem);

		/* <Tests purposes only> */
		if (pItem.ItmType == Item.ItemType.Weapon)
			pEquipedWeapon = pItem as Weapon;
		else if (pItem.ItmType == Item.ItemType.Armor)
			pEquipedArmor = pItem as Armor;
		/* </Tests> */
	}


	public List<Item> Items
	{
		get { return pItems; }
	}

	public Weapon EquipedWeapon
	{
		get { return pEquipedWeapon; }
	}

	public Armor EquipedArmor
	{
		get { return pEquipedArmor; }
	}
}
