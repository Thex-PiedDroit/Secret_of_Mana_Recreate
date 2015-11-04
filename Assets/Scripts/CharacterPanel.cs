using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterPanel : ManaPanel
{
	#region Variables (private)

	private Text pName;
	private Text pLevel;
	private Text pHP;
	private Text pMP;
	private Text pAtk;
	private Text pAtkBonus;
	private Text pDef;
	private Text pDefBonus;
	
	#endregion

	override public void Initialize()
	{
		base.Initialize();

		m_pOpenButton = "CharacPanel";
		pName = GameObject.Find("CharName").GetComponent<Text>();
		pLevel = GameObject.Find("LevelValue").GetComponent<Text>();
		pHP = GameObject.Find("HPValue").GetComponent<Text>();
		pMP = GameObject.Find("MPValue").GetComponent<Text>();
		pAtk = GameObject.Find("AtkValue").GetComponent<Text>();
		pAtkBonus = GameObject.Find("AtkBonusValue").GetComponent<Text>();
		pDef = GameObject.Find("DefValue").GetComponent<Text>();
		pDefBonus = GameObject.Find("DefBonusValue").GetComponent<Text>();
	}

	override public void Refresh()
	{
		VisualCharacter pCurrentChar = CurrentChar;

		pName.text = pCurrentChar.Name;
		pLevel.text = pCurrentChar.LevelText;
		pHP.text = pCurrentChar.HPText;
		pMP.text = pCurrentChar.MPText;
		pAtk.text = pCurrentChar.AtkText;
		pAtkBonus.text = pCurrentChar.AtkBonusText;
		pDef.text = pCurrentChar.DefText;
		pDefBonus.text = pCurrentChar.DefBonusText;
	}
}
