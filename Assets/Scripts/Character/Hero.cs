using UnityEngine;
using System;
using System.Collections;

public class Hero : Character
{
	#region Variables (private)

	private float m_fFollowDist = 2.0f;

	private int m_iLvl = 1;
	private int m_iMPMax = 10;
	private int m_iMP = 10;
	private bool m_bSelected = false;
	
	#endregion

	public Hero(string pName, Vector3 tPosition, Side eSide) : base(pName, tPosition, eSide)
	{

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
			if (m_pInventory.EquipedWeapon != null)
			{
				m_pInventory.EquipedWeapon.Use();
				OnAttack(); // This triggers the action and notify all that are listening
			}
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

	public int Level
	{
		get { return m_iLvl; }
	}

	public int CurrentMP
	{
		get { return m_iMP; }
	}

	public int MPMax
	{
		get { return m_iMPMax; }
	}

	public bool Selected
	{
		set { m_bSelected = value; }
		get { return m_bSelected; }
	}

	#endregion Setters/Getters
}
