using UnityEngine;
using System.Collections;
using System;

public class Character
{
    #region Variables (private)

    //private VisualCharacter m_pCharacterPRES = null; NOT ALLOWED Logic does not know visual
    private Vector3 _position = Vector3.zero;
    private Vector3 _forward = Vector3.forward;
    private Vector3 _destination = Vector3.zero;
    public Action OnHitTaken; //Visual character can register to this event

	private string m_pName = "Anon";

	private float m_fSpeed = 10.0f;
	private float m_fFollowDist = 2.0f;

	private int m_iLvl = 1;
	private int m_iHP = 50;
	private int m_iMP = 10;
	private int m_iAtk = 5;
	private int m_iDef = 5;
    private bool _selected = false;
	
	#endregion

	public Character(string pName)//, VisualCharacter pCharacterPRES)
	{
		m_pName = pName;
		//m_pCharacterPRES = pCharacterPRES;
	}
    public Character(string pName, Vector3 position)//add more if needed but this should be enough
    {
        m_pName = pName;
        _position = position;
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

            //use the vector3 
            //m_pCharacterPRES.Forward = tMove;
            //m_pCharacterPRES.transform.Translate(tMove * Time.deltaTime, Space.World);

            _forward = tMove;
            _position += tMove * Time.deltaTime;
        }

		if (Input.GetButtonDown("Hit"))
		{
            OnHitTaken(); // This triggers the action and notify all that are listening
			//m_pCharacterPRES.AttackAnim();
		}
	}

	public void FollowSelected(Vector3 tSelectedHeroPos)
	{
        //Vector3 tCurrentPos = m_pCharacterPRES.Pos;
        /*
		if ((tSelectedHeroPos - tCurrentPos).sqrMagnitude > (m_fFollowDist * m_fFollowDist))
			m_pCharacterPRES.Destination = tSelectedHeroPos;
		else if (m_pCharacterPRES.HasPath)
			m_pCharacterPRES.Destination = tCurrentPos;
        /**/
    }
    public void FollowSelected(Character selectedHero)
    {
        //don't follow yourself
        if (selectedHero == this)
            return;
        //possible solution to have Follow without access to visual character
        if ((selectedHero.Position - _position).sqrMagnitude > (m_fFollowDist * m_fFollowDist))
            _destination = selectedHero.Position;
        else 
            _destination = _position;
    }

    #endregion Methods


    #region Setters/Getters

    public string Name
	{
		set { m_pName = value; } //DO NOT CREATE SETTERS IF THEY ARE NOT NEEDED
        get { return m_pName; }
	}

	public int Level
	{
		set { m_iLvl = value; } //DO NOT CREATE SETTERS IF THEY ARE NOT NEEDED
        get { return m_iLvl; }
	}

	public int HP
	{
		set { m_iHP = value; } //DO NOT CREATE SETTERS IF THEY ARE NOT NEEDED
        get { return m_iHP; }
	}

	public int MP
	{
		set { m_iMP = value; } //DO NOT CREATE SETTERS IF THEY ARE NOT NEEDED
        get { return m_iMP; }
	}

	public int Attack
	{
		set { m_iAtk = value; } //DO NOT CREATE SETTERS IF THEY ARE NOT NEEDED
        get { return m_iAtk; }
	}

	public int Def
	{
		set { m_iDef = value; } //DO NOT CREATE SETTERS IF THEY ARE NOT NEEDED
		get { return m_iDef; }
    }
    public Vector3 Position
    {
        get
        {
            return _position;
        }
    }
    public Vector3 Forward
    {
        get
        {
            return _forward;
        }
    }
    public Vector3 Destination
    {
        get
        {
            return _destination;
        }
    }
    public bool Selected
    {
        get
        {
            return _selected;
        }
        set
        {
            _selected = value;
        }
    }
    #endregion Setters/Getters
}
