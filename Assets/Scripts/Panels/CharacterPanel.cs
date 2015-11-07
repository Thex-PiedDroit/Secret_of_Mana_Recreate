using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterPanel : ManaPanel
{
	#region Variables (public)

	[SerializeField]
	private Text pLevel;
	[SerializeField]
	private Text pHP;
	[SerializeField]
	private Text pMP;
	[SerializeField]
	private Text pAtk;
	[SerializeField]
	private Text pAtkBonus;
	[SerializeField]
	private Text pDef;
	[SerializeField]
	private Text pDefBonus;
	
	#endregion

	override public void Refresh()
	{
		VisualHero pCurrentChar = CurrentPageChar;

		pName.text = pCurrentChar.Name;
		pLevel.text = pCurrentChar.LevelText;
		pHP.text = pCurrentChar.HPText;
		pMP.text = pCurrentChar.MPText;
		pAtk.text = pCurrentChar.AtkText;
		pAtkBonus.text = pCurrentChar.AtkBonusText;
		pDef.text = pCurrentChar.DefText;
		pDefBonus.text = pCurrentChar.DefBonusText;
	}


	public override void CatchOpenInput()
	{
		base.CatchOpenInput();

		if (Input.GetButtonDown("Select"))
			SelectHero();
	}

	public void SelectHero()
	{
		s_iSelectedCharPage = m_iCurrentPage;
		GameManager.Inst.CharManager.SelectHero(m_iCurrentPage);
	}
}
