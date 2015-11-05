using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
	#region Variables (private)

	private Vector3 m_tSpawnPos = Vector3.zero;
	private float m_fMaxDist = 50.0f;
	private float m_fSpeed = 30.0f;
	private int m_iDamages = 0;

	private Character.Side m_eSide = Character.Side.GoodGuys;
	
	#endregion


	void Start()
	{
		m_tSpawnPos = transform.position;
	}

	void Update()
	{
		Vector3 tMove = (transform.forward * m_fSpeed) * CharacterManager.DeltaTime;	// Took the liberty to merge visual and logic for this class because of how small it is
		transform.Translate(tMove, Space.World);

		if ((transform.position - m_tSpawnPos).sqrMagnitude >= (m_fMaxDist * m_fMaxDist))
			Destroy(gameObject);
	}


	void OnCollisionEnter(Collision tCollider)
	{
		print(tCollider.gameObject.name);
		VisualCharacter tHitCharPRES = tCollider.transform.parent.gameObject.GetComponent<VisualCharacter>();

		if (tHitCharPRES != null && tHitCharPRES.CharacterBUS.CharSide != m_eSide)
			tHitCharPRES.CharacterBUS.Damage(m_iDamages);

		Destroy(gameObject);
	}


	public Character.Side ArrowSide
	{
		set { m_eSide = value; }
	}

	public int Damages
	{
		set { m_iDamages = value; }
	}
}
