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
		if (!m_bPause && m_pEnemyBUS.IsAlive)
		{
			base.Update();

			if (m_pRenderer.isVisible)
				m_pEnemyBUS.Update();
		}

		else if (!m_pEnemyBUS.IsAlive && !m_pRenderer.isVisible)	// If dead and out of screen
			Destroy(gameObject);
	}
}
