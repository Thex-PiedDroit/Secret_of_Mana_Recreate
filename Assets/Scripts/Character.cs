using UnityEngine;
using System.Collections;

public class Character
{
#region Variables (private)

	private VisualCharacter m_pCharacter = null;

	private string m_pName = "Anon";

	private float m_fSpeed = 10.0f;

	private int m_iLvl = 1;
	private int m_iHP = 50;
	private int m_iMP = 10;
	private int m_iAtk = 5;
	private int m_iDef = 5;
	
	#endregion

	public Character(string pName, VisualCharacter pCharacter)
	{
		m_pName = pName;
		m_pCharacter = pCharacter;
	}

#region Methods

	public void FixedUpdate()
	{
		float fMoveV = Input.GetAxis("Vertical");
		float fMoveH = Input.GetAxis("Horizontal");

		if (fMoveV != 0.0f || fMoveH != 0.0f)
		{
			Vector3 tMove = new Vector3(fMoveH, 0.0f, fMoveV);

			if (tMove.sqrMagnitude > 1.0f)
				tMove.Normalize();
			tMove *= m_fSpeed;

			m_pCharacter.Forward = tMove;
			m_pCharacter.transform.Translate(tMove * Time.deltaTime, Space.World);
		}

		if (Input.GetButtonDown("Hit"))
		{
			m_pCharacter.Attack();
		}
	}

	public void Update()
	{

	}

	#endregion Methods


#region Setters/Getters

	public string Name
	{
		set { m_pName = value; }
		get { return m_pName; }
	}

	public int Level
	{
		set { m_iLvl = value; }
		get { return m_iLvl; }
	}

	public int HP
	{
		set { m_iHP = value; }
		get { return m_iHP; }
	}

	public int MP
	{
		set { m_iMP = value; }
		get { return m_iMP; }
	}

	public int Attack
	{
		set { m_iAtk = value; }
		get { return m_iAtk; }
	}

	public int Def
	{
		set { m_iDef = value; }
		get { return m_iDef; }
	}

	#endregion Setters/Getters
}
