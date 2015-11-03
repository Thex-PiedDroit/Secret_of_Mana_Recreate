using UnityEngine;
using System.Collections;

public class CharacterManager
{
#region Variables (private)

	static private CharacterManager s_pInst = null;

	private VisualCharacter[] m_pHeroesPRES = new VisualCharacter[3];
	private Character[] m_pHeroesBUS = null;
	private int m_iSelectedHero = 0;
	
	#endregion


	static public CharacterManager Inst
	{
		get
		{
			if (s_pInst == null)
				s_pInst = new CharacterManager();

			return s_pInst;
		}
	}

	private CharacterManager()
	{
		m_pHeroesPRES = Resources.LoadAll<VisualCharacter>("Heroes");
		m_pHeroesBUS = new Character[m_pHeroesPRES.Length];
	}


	public void FixedUpdate()
	{
		m_pHeroesBUS[m_iSelectedHero].CatchInputs();
	}


	public VisualCharacter[] HeroesPRES
	{
		get { return m_pHeroesPRES; }
	}

	public Character[] HeroesBUS
	{
		get { return m_pHeroesBUS; }
	}
}
