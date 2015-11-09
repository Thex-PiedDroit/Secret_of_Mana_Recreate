using UnityEngine;
using System.Collections;

public class Sword : Weapon
{
#region Variables (private)

	static private GameObject s_pSwordRef = null;
	static private GameObject s_pSwordIconRef = null;

	#endregion


	static public void S_SwordInitialize()
	{
		s_pSwordRef = Resources.Load<GameObject>("Heroes/Weapons/Sword");
		s_pSwordIconRef = Resources.Load<GameObject>("HUD/ItemIcons/SwordIcon");
	}


	public Sword(Character pUser) : base(pUser, WeaponType.Sword)
	{
		m_iTargetsLayer = ~(LayerMask.GetMask("Heroes"));
		m_fRange = 3.0f;
		m_iAtk = 5;
	}

	public override void Use()
	{
		Collider[] tHitColliders = Physics.OverlapSphere(m_pUser.Position + Vector3.up, m_fRange, m_iTargetsLayer, QueryTriggerInteraction.Ignore);

		for (int i = 0; i < tHitColliders.Length; i++)
		{
			VisualCharacter tHitChar = tHitColliders[i].transform.parent.GetComponent<VisualCharacter>();

			if (tHitChar != null)
			{
				Vector3 tSelfToHit = tHitColliders[i].transform.position - m_pUser.Position;

				if (tHitChar.CharacterBUS != m_pUser && tHitChar.CharacterBUS.CharSide != m_pUser.CharSide &&
					Vector3.Dot(tSelfToHit, m_pUser.Forward) > 0.0f)
				{
					tHitChar.CharacterBUS.Damage(m_pUser.Atk + m_iAtk);
				}
			}
		}
	}


	override public GameObject IconRef
	{
		get { return s_pSwordIconRef; }
	}

	static public GameObject Ref
	{
		get { return s_pSwordRef; }
	}
}
