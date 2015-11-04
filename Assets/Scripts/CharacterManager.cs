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


	public void Update()
	{
		for (int i = 0; i < m_pHeroesBUS.Length; i++)
		{
			if (i == m_iSelectedHero)
				m_pHeroesBUS[i].CatchInputs();
			else
				m_pHeroesBUS[i].FollowSelected(m_pHeroesPRES[m_iSelectedHero].Pos);
		}
	}


	public VisualCharacter[] HeroesPRES
	{
		get { return m_pHeroesPRES; }
	}

	public Character[] HeroesBUS
	{
		get { return m_pHeroesBUS; }
	}

	public Vector3 SelectedHeroPos
	{
		get { return m_pHeroesPRES[m_iSelectedHero].transform.position; }
	}
}
