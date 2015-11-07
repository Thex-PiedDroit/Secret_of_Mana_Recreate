using UnityEngine;
using System;
using System.Collections;

public class Character
{
#region Variables (public)

	public enum Side
	{
		Default,
		GoodGuys,
		BadGuys
	}

	public Action OnAttack; //Visual character can register to this event
	public Action OnHealthChanged;

	#endregion

#region Variables (protected)

	protected Vector3 m_tPosition = Vector3.zero;
	protected Vector3 m_tForward = Vector3.forward;
	protected Vector3 m_tDestination = Vector3.zero;

	protected Inventory m_pInventory = null;

	protected float m_fSpeed = 10.0f;
	protected float m_fFollowDist = 2.0f;

	protected bool m_bDead = false;

	#endregion

#region Variables (private)

	private string m_pName = "Anon";

	private Side m_eSide = Side.Default;

	private int m_iHPMax = 50;
	private int m_iLastHPChange = 0;
	private int m_iHP = 50;
	private int m_iAtk = 5;
	private int m_iDef = 0;
	
	#endregion

	public Character(string pName, Vector3 tPosition, Side eSide)//add more if needed but this should be enough
	{
		m_pInventory = new Inventory();
		m_pName = pName;
		m_tPosition = tPosition;
		m_eSide = eSide;
	}

#region Methods

	public void Heal(int iHeal)
	{
		if (m_iHP + iHeal > m_iHPMax)
			iHeal = (m_iHPMax - m_iHP);

		m_iHP += iHeal;

		m_iLastHPChange = iHeal;

		OnHealthChanged();
	}

	virtual public void Damage(int iDamages)
	{
		iDamages -= m_pInventory.EquipedArmor.Def;
		if (iDamages < 0)
			iDamages = 0;

		m_iLastHPChange = -iDamages;

		m_iHP -= iDamages;

		if (m_iHP <= 0)
		{
			m_iHP = 0;
			m_bDead = true;
		}

		OnHealthChanged();
	}

	#endregion Methods


#region Getters/Setters

	public Side CharSide
	{
		//set { m_eSide = value; }
		get { return m_eSide; }
	}

	public bool IsAlive
	{
		get { return !m_bDead;}
	}

	public Inventory Inv
	{
		get { return m_pInventory; }
	}

	public Weapon WeaponEquiped
	{
		set { m_pInventory.AddItem(value); }
	}

	public Armor ArmorEquiped
	{
		set { m_pInventory.AddItem(value); }
	}

	public string Name
	{
		get { return m_pName; }
	}

	public int CurrentHP
	{
		get { return m_iHP; }
	}

	public int HPMax
	{
		get { return m_iHPMax; }
	}

	public int LastHPChange
	{
		get { return m_iLastHPChange; }
	}

	public int Atk
	{
		get { return m_iAtk; }
	}

	public int WpnAtk
	{
		get { return m_pInventory.EquipedWeapon != null ? m_pInventory.EquipedWeapon.Atk : 0; }
	}

	public int Def
	{
		get { return m_iDef; }
	}

	public int ArmorDef
	{
		get { return m_pInventory.EquipedArmor != null ? m_pInventory.EquipedArmor.Def : 0; }
	}

	public Vector3 Position
	{
		set
		{
			m_tPosition = value;
		}

		get
		{
		 return m_tPosition;
		}
	}

	public Vector3 Forward
	{
		set
		{
			m_tForward = value;
		}

		get
		{
		 return m_tForward;
		}
	}

	public Vector3 Destination
	{
		get
		{
			return m_tDestination;
		}
	}

	#endregion Setters/Getters
}
