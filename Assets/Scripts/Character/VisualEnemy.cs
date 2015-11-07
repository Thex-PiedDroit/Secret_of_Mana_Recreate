using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisualEnemy : VisualCharacter
{
	#region Variables (public)

	[SerializeField]
	private Transform m_pHealthBar = null;
	
	#endregion
	
	#region Variables (private)

	private Enemy m_pEnemyBUS = null;
	static private Hero s_pActiveHero = null;
	
	#endregion


	public override void Initialize(Character pCharacter)
	{
		base.Initialize(pCharacter);
		m_pEnemyBUS = pCharacter as Enemy;
	}

	override public void Update()
	{
		base.Update();
	}




	static public Hero ActiveHero
	{
		set { s_pActiveHero = value; }
	}
}
