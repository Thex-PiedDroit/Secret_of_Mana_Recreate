using UnityEngine;
using System.Collections;

public class VisualArrow : MonoBehaviour
{
	#region Variables (private)

	private Arrow m_pArrowBUS = null;
	
	#endregion


	void Start()
	{
		transform.forward = m_pArrowBUS.Forward;
	}

	void Update()
	{
		m_pArrowBUS.Update();
		transform.position = m_pArrowBUS.Position;

		if (m_pArrowBUS.IsDead)
			Destroy(gameObject);
	}


	void OnCollisionEnter(Collision tCollider)
	{
		VisualCharacter tHitCharPRES = tCollider.gameObject.GetComponent<VisualCharacter>();
		Character tVisualChar = tHitCharPRES != null ? tHitCharPRES.CharacterBUS : null;

		m_pArrowBUS.OnCollisionEnter(tVisualChar);
	}


	public Arrow ArrowBUS
	{
		set { m_pArrowBUS = value; }
	}
}
