using UnityEngine;
using System.Collections;

public class Arrow
{
	#region Variables (private)

	private Vector3 m_tPos = Vector3.zero;
	private Vector3 m_tSpawnPos = Vector3.zero;
	private Vector3 m_tForward = Vector3.forward;
	private float m_fMaxDist = 50.0f;
	private float m_fSpeed = 30.0f;
	private int m_iDamages = 0;
	private bool m_bDead = false;

	private Character.Side m_eSide = Character.Side.GoodGuys;
	
	#endregion


	public Arrow(Vector3 tPos, Vector3 tForward, int iDamages, float fMaxDist, Character.Side eArrowSide)
	{
		m_tPos = tPos;
		m_tSpawnPos = tPos;
		m_tForward = tForward.normalized;
		m_iDamages = iDamages;
		m_fMaxDist = fMaxDist;
		m_eSide = eArrowSide;
	}

	public void Update()
	{
		Vector3 tMove = (m_tForward * m_fSpeed) * CharacterManager.DeltaTime;
		m_tPos += tMove;

		if ((m_tPos - m_tSpawnPos).sqrMagnitude >= (m_fMaxDist * m_fMaxDist))
			m_bDead = true;
	}


	public void OnCollisionEnter(Character pCharacterHit)
	{
		if (pCharacterHit != null &&
			pCharacterHit.CharSide != m_eSide)
		{
			pCharacterHit.Damage(m_iDamages);
		}

		m_bDead = true;
	}


	public Vector3 Position
	{
		get { return m_tPos; }
	}

	public Vector3 Forward
	{
		get { return m_tForward; }
	}

	public bool IsDead
	{
		get { return m_bDead; }
	}
}
