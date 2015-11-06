using UnityEngine;
using System.Collections;

public class Bow : Weapon
{
	#region Variables (private)

	static private GameObject s_pBowRef = null;
	static private GameObject s_pBowIconRef = null;
	static private VisualArrow s_pArrowRef = null;

	#endregion


	static public void S_BowInitialize()
	{
		s_pBowRef = Resources.Load<GameObject>("Heroes/Weapons/Bow");
		s_pBowIconRef = Resources.Load<GameObject>("HUD/ItemIcons/BowIcon");
		s_pArrowRef = Resources.Load<VisualArrow>("Heroes/Weapons/Arrow");
	}


	public Bow(Character pUser) : base(pUser, WeaponType.Bow)
	{
		m_iTargetsLayer = ~(LayerMask.GetMask("Heroes"));
		m_fRange = 50.0f;
		m_iAtk = 2;
	}

	public override void Use()
	{
		Vector3 tArrowPos = m_pUser.Position + (m_pUser.Forward.normalized * 2.0f) + Vector3.up;
		Arrow pArrow = new Arrow(tArrowPos, m_pUser.Forward, m_pUser.Atk + m_iAtk, m_fRange, m_pUser.CharSide);
		VisualArrow pVisualArrow = GameObject.Instantiate(s_pArrowRef, tArrowPos, Quaternion.identity) as VisualArrow;	// I know this should go in a VisualBow.cs but as it would be for one line, I figured it's forgetable, this time
		pVisualArrow.ArrowBUS = pArrow;
	}


	override public GameObject IconRef
	{
		get { return s_pBowIconRef; }
	}

	static public GameObject Ref
	{
		get { return s_pBowRef; }
	}
}
