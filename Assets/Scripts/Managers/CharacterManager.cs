using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Assertions;

public class CharacterManager
{
#region Variables (private)

	private List<VisualCharacter> m_pHeroesPRES = null;
	private List<Character> m_pHeroesBUS = null;
	private Character m_pSelectedHero = null;
	private Transform pCharactersContainer = null;

	private bool m_bPaused = false;
	private bool m_bGameOver = false;

	static private float s_fGameTimeScale = 1.0f;
	
	#endregion
	

	public CharacterManager()
	{
		Weapon.S_Initialize();

		pCharactersContainer = GameObject.Find("Heroes").transform;
		m_pHeroesBUS = new List<Character>(3);
		m_pHeroesBUS.Add(new Character("Lucia", Vector3.zero, Character.Side.GoodGuys));
		m_pHeroesBUS[0].WeaponEquiped = new Sword(m_pHeroesBUS[0]);
		m_pHeroesBUS.Add(new Character("Richard", Vector3.one, Character.Side.GoodGuys));
		m_pHeroesBUS[1].WeaponEquiped = new Bow(m_pHeroesBUS[1]);
		m_pHeroesBUS.Add(new Character("Nataly", -Vector3.one, Character.Side.GoodGuys));
		m_pHeroesBUS[2].WeaponEquiped = new Staff(m_pHeroesBUS[2]);
		for (int i = 0; i < m_pHeroesBUS.Count; i++)
			m_pHeroesBUS[i].OnDeath += DeadHeroHandle;

		m_pHeroesPRES = new List<VisualCharacter>(3);

		GameObject pRes = Resources.Load("Heroes/Lucia") as GameObject;
		CreateHero(pRes, m_pHeroesBUS[0]);
		CreateHero(pRes, m_pHeroesBUS[1]);
		CreateHero(pRes, m_pHeroesBUS[2]);

		m_pSelectedHero = m_pHeroesBUS[0];
		m_pHeroesBUS[0].Selected = true;
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
		if (!m_bGameOver && !m_bPaused)
		{
			m_pSelectedHero.CatchInputs();
			m_pHeroesBUS[0].FollowSelected(m_pSelectedHero);
			m_pHeroesBUS[1].FollowSelected(m_pSelectedHero);
			m_pHeroesBUS[2].FollowSelected(m_pSelectedHero);
		}
	}

	public void DeadHeroHandle()
	{
		bool bSelectOtherHero = !m_pSelectedHero.IsAlive;
		bool bGameOver = true;

		for (int i = 0; i < m_pHeroesBUS.Count; i++)
		{
			if (m_pHeroesBUS[i].IsAlive)
			{
				bGameOver = false;

				if (bSelectOtherHero)
					SelectHero(i);

				break;
			}
		}

		m_bGameOver = bGameOver;
	}

	public void SelectHero(int iHeroID)
	{
		if (m_pHeroesBUS[iHeroID].IsAlive)
		{
			m_pSelectedHero.Selected = false;
			m_pSelectedHero = m_pHeroesBUS[iHeroID];
			m_pHeroesPRES[iHeroID].ToggleSelect();
		}
	}


#region CallBacks

	public void TogglePause()
	{
		m_bPaused = !m_bPaused;

		for (int i = 0; i < m_pHeroesPRES.Count; i++)
			m_pHeroesPRES[i].TogglePause();
	}

	#endregion CallBacks

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
