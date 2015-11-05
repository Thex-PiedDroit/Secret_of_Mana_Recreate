using UnityEngine;
using System.Collections;

public class Bow : Weapon
{
	#region Variables (private)

	static private GameObject s_pBowRef;
	static private GameObject s_pArrowRef;

	#endregion


	static public void S_BowInitialize()
	{
		s_pBowRef = Resources.Load<GameObject>("Heroes/Weapons/Bow");
		s_pArrowRef = Resources.Load<GameObject>("Heroes/Weapons/Arrow");
	}


	public Bow(Character pUser) : base(pUser, WeaponType.Bow)
	{
		m_iTargetsLayer = ~(LayerMask.GetMask("Heroes"));
		m_fRange = 10.0f;
		m_iAtk = 2;
	}

	public override void Use()
	{
		GameObject pArrow = MonoBehaviour.Instantiate(s_pArrowRef, m_pUser.Position + (m_pUser.Forward.normalized * 2.0f) + Vector3.up, Quaternion.identity) as GameObject;
		pArrow.GetComponent<Arrow>().Damages = m_pUser.Atk + m_iAtk;
		pArrow.transform.forward = m_pUser.Forward;
	}


	static public GameObject Ref
	{
		get { return s_pBowRef; }
	}
}
