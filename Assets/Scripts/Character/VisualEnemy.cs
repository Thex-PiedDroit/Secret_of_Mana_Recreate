using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisualEnemy : VisualCharacter
{
#region Variables (private)

	private Enemy m_pEnemyBUS = null;
	
	#endregion


	public override void Initialize(Character pCharacter)
	{
		base.Initialize(pCharacter);
		m_pEnemyBUS = pCharacter as Enemy;
	}

	public override void Update()
	{
		if (!m_bPause)
		{
			base.Update();

			if (m_pRenderer.isVisible && m_pEnemyBUS.IsAlive)
				m_pEnemyBUS.Update();
		}

		if (!m_pEnemyBUS.IsAlive && !m_pRenderer.isVisible)	// If dead and out of screen
			Destroy(gameObject);
	}
}
