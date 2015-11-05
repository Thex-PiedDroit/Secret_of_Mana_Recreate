using UnityEngine;
using System.Collections;

public class Staff : Weapon
{
#region Variables (private)

	private int m_iHeal = 3;

	#endregion


	public Staff(Character pUser) : base(pUser, WeaponType.Staff)
	{
		m_iTargetsLayer = LayerMask.GetMask("Heroes");
		m_fRange = 5.0f;
		m_iAtk = 0;
	}

	public override void Use()
	{
		RaycastHit Hit;

		if (Physics.Raycast(m_pUser.Position + Vector3.up, m_pUser.Forward, out Hit, m_fRange, m_iTargetsLayer, QueryTriggerInteraction.Ignore))
		{
			VisualCharacter tChar = Hit.collider.transform.parent.gameObject.GetComponent<VisualCharacter>();
			if (tChar != null)
				tChar.CharacterBUS.Heal(m_iHeal);
		}
	}
}
