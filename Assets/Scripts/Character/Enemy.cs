using UnityEngine;
using System.Collections;

public class Enemy : Character
{
#region Variables (private)

	static private Hero s_pActiveHero = null;

	private float m_fAttackRange = 3.0f;
	private float m_fSightRange = 5.0f;

	private float m_fTimeBetweenAttacks = 1.0f;
	private float m_fAttackTimer = 0.0f;

	private int m_iAtk = 10;

	private bool m_bHasTarget = false;
	private bool m_bPursuingTarget = false;
	
	#endregion


	public Enemy(string pName, Vector3 tPosition, Side eSide) : base(pName, tPosition, eSide)
	{

	}

	public void Update()
	{
		float fSqrdDistToHero = (s_pActiveHero.Position - m_tPosition).sqrMagnitude;

		if (!m_bPursuingTarget)
			FindTarget(fSqrdDistToHero);

		if (m_bHasTarget)
			AttackTarget(fSqrdDistToHero);
	}

	void FindTarget(float fSqrdDistToHero)
	{
		if (s_pActiveHero.IsAlive)
			m_bHasTarget = (fSqrdDistToHero <= (m_fSightRange * m_fSightRange));	// As no other target available than active hero, a boolean is currently enough
		else
			m_bHasTarget = false;
	}

	void AttackTarget(float fSqrdDistToHero)
	{
		if (fSqrdDistToHero <= (m_fAttackRange * m_fAttackRange))
		{
			m_tDestination = m_tPosition;
			m_tForward = s_pActiveHero.Position - m_tPosition;
			m_bPursuingTarget = false;

			if (Time.fixedTime - m_fAttackTimer >= m_fTimeBetweenAttacks)
			{
				s_pActiveHero.Damage(m_iAtk);
				m_fAttackTimer = Time.fixedTime;
			}
		}

		else
			m_tDestination = s_pActiveHero.Position;
	}

	override public void Damage(int iDamages)
	{
		base.Damage(iDamages);

		if (!m_bHasTarget)
		{
			m_bHasTarget = true;
			m_bPursuingTarget = true;
		}
	}


	public float SightRange
	{
		set { m_fSightRange = value; }
	}

	static public Hero ActiveHero
	{
		set { s_pActiveHero = value; }
	}
}
