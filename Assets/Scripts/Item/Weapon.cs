using UnityEngine;
using System.Collections;

abstract public class Weapon : Item
{
	public enum WeaponType
	{
		Default,
		Sword,
		Bow,
		Staff
	}

	#region Variables (protected)

	protected float m_fRange = 1.0f;
	protected int m_iAtk = 0;

	protected int m_iTargetsLayer = ~0;
	
	#endregion

	#region Variables (private)

	private WeaponType m_eWeaponType = WeaponType.Default;

	#endregion


	static public void S_Initialize()
	{
		Bow.BowInitialize();
	}

	public Weapon(Character pUser, WeaponType eWeaponType) : base(pUser, ItemType.Weapon)
	{
		m_eWeaponType = eWeaponType;
	}


	void Start()
	{
		m_eItemType = ItemType.Weapon;
	}


	abstract public void Use();


	public int Atk
	{
		set { m_iAtk = value; }
		get { return m_iAtk; }
	}
}
