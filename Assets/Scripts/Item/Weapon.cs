using UnityEngine;
using System.Collections;

abstract public class Weapon : Item
{
	#region Variables (protected)

	protected float m_fRange = 1.0f;
	protected int m_iAtk = 0;

	protected int m_iTargetsLayer = ~0;
	
	#endregion


	static public void S_Initialize()
	{
		Bow.BowInitialize();
	}

	public Weapon(Character pUser)
	{
		m_pUser = pUser;
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
