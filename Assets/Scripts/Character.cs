using UnityEngine;
using System.Collections;
using System;

public class Character
{
#region Variables (public)

	public Action OnHitTaken; //Visual character can register to this event

	#endregion

#region Variables (private)

	//private VisualCharacter m_pCharacterPRES = null; NOT ALLOWED Logic does not know visual
	private Vector3 m_tPosition = Vector3.zero;
	private Vector3 m_tForward = Vector3.forward;
	private Vector3 m_tDestination = Vector3.zero;

	private string m_pName = "Anon";

	private float m_fSpeed = 10.0f;
	private float m_fFollowDist = 2.0f;

	private int m_iLvl = 1;
	private int m_iHPMax = 50;
	private int m_iHP = 50;
	private int m_iMPMax = 10;
	private int m_iMP = 10;
	private int m_iAtk = 5;
	private int m_iDef = 5;
	private bool m_bSelected = false;
	
	#endregion

	public Character(string pName, Vector3 tPosition)//add more if needed but this should be enough
	{
		m_pName = pName;
		m_tPosition = tPosition;
	}

#region Methods

	public void CatchInputs()
	{
		float fMoveV = Input.GetAxis("Vertical");
		float fMoveH = Input.GetAxis("Horizontal");

		if (fMoveV != 0.0f || fMoveH != 0.0f)
		{
			Vector3 tMove = new Vector3(fMoveH, 0.0f, fMoveV);

			if (tMove.sqrMagnitude > 1.0f)
				tMove.Normalize();
			tMove *= m_fSpeed;

			m_tForward = tMove;
			m_tPosition += tMove * CharacterManager.DeltaTime;
		}

		if (Input.GetButtonDown("Hit"))
		{
			OnHitTaken(); // This triggers the action and notify all that are listening
		}
	}

	public void FollowSelected(Character pSelectedHero)
	{
		//don't follow yourself
		if (pSelectedHero == this)
			return;

		//possible solution to have Follow without access to visual character
			// ==> Made visualCharacter update logic Character's forward and pos if not selected

		if ((pSelectedHero.Position - m_tPosition).sqrMagnitude > (m_fFollowDist * m_fFollowDist))
			m_tDestination = pSelectedHero.Position;

		else
			m_tDestination = m_tPosition;
	}

	#endregion Methods


#region Getters/Setters

	public string Name
	{
		get
		{
			return m_pName;
		}
	}

	public int Level
	{
		get { return m_iLvl; }
	}

	public int CurrentHP
	{
		get { return m_iHP; }
	}

	public int HPMax
	{
		get { return m_iHPMax; }
	}

	public int CurrentMP
	{
		get { return m_iMP; }
	}

	public int MPMax
	{
		get { return m_iMPMax; }
	}

	public int Atk
	{
		get { return m_iAtk /* - pWeapon.Atk*/; }
	}

	public int WpnAtk
	{
		get { return 0; }	// Currently empty
	}

	public int Def
	{
		get { return m_iDef /* - pArmor.Def*/; }
	}

	public int ArmorDef
	{
		get { return 0; }	//Currently empty
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
	
	public bool Selected
	{
		get
		{
			return m_bSelected;
		}
		
		set
		{
			m_bSelected = value;
		}
	}
	#endregion Setters/Getters
}
