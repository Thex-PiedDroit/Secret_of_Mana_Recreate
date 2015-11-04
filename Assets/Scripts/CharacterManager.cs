using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterManager
{
#region Variables (private)

	private List<VisualCharacter> m_pHeroesPRES = null;
	private Character pHero1 = null;
	private Character pHero2 = null;
	private Character pHero3 = null;
	private Character m_pSelectedHero = null;
	private Transform pCharactersContainer = null;

	private bool m_bPaused = false;

	static private float s_fGameTimeScale = 1.0f;
	
	#endregion
	

	public CharacterManager()
	{
		pCharactersContainer = GameObject.Find("Heroes").transform;
		pHero1 = new Character("Lucia", Vector3.zero);
		pHero2 = new Character("Richard", Vector3.one);
		pHero3 = new Character("Nataly", -Vector3.one);
		m_pHeroesPRES = new List<VisualCharacter>(3);

		GameObject pRes = Resources.Load("Heroes/Lucia") as GameObject;
		CreateHero(pRes, pHero1);
		CreateHero(pRes, pHero2);
		CreateHero(pRes, pHero3);

		m_pSelectedHero = pHero1;
		pHero1.Selected = true;
	}

	private void CreateHero(GameObject pRes, Character pHero)
	{
		GameObject pVisualHero = GameObject.Instantiate(pRes) as GameObject;
		VisualCharacter pVisualChar = pVisualHero.GetComponent<VisualCharacter>();
		if (pVisualChar != null)
		{
			pVisualChar.Initialize(pHero);
			m_pHeroesPRES.Add(pVisualChar);
			pVisualHero.transform.parent = pCharactersContainer;
		}
	}

	public void Update()
	{
		if (!m_bPaused)
		{
			m_pSelectedHero.CatchInputs();
			pHero1.FollowSelected(m_pSelectedHero);
			pHero2.FollowSelected(m_pSelectedHero);
			pHero3.FollowSelected(m_pSelectedHero);
		}
	}


	public void TogglePause()
	{
		m_bPaused = !m_bPaused;

		for (int i = 0; i < m_pHeroesPRES.Count; i++)
			m_pHeroesPRES[i].TogglePause();
	}


#region Getters/Setters

	public Character SelectedHero
	{
		get { return m_pSelectedHero; }
	}

	public List<VisualCharacter> VisualCharactersList
	{
		get { return m_pHeroesPRES; }
	}

	static public float TimeScale
	{
		set { s_fGameTimeScale = value; }
		get { return s_fGameTimeScale; }
	}

	static public float DeltaTime
	{
		get { return Time.deltaTime * s_fGameTimeScale; }
	}

	#endregion Getters/Setters
}
