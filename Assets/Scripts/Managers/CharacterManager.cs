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
		m_pHeroesBUS.Add(new Character("Richard", Vector3.one, Character.Side.GoodGuys));
		m_pHeroesBUS.Add(new Character("Nataly", -Vector3.one, Character.Side.GoodGuys));
		for (int i = 0; i < m_pHeroesBUS.Count; i++)
			m_pHeroesBUS[i].OnDeath += DeadHeroHandle;

		m_pHeroesPRES = new List<VisualCharacter>(3);

		GameObject pRes = Resources.Load("Heroes/Hero") as GameObject;
		CreateHero(pRes, m_pHeroesBUS[0], Weapon.WeaponType.Sword, Armor.ArmorType.Heavy);
		CreateHero(pRes, m_pHeroesBUS[1], Weapon.WeaponType.Bow, Armor.ArmorType.Medium);
		CreateHero(pRes, m_pHeroesBUS[2], Weapon.WeaponType.Staff, Armor.ArmorType.Light);

		m_pSelectedHero = m_pHeroesBUS[0];
		m_pHeroesBUS[0].Selected = true;
	}

	private void CreateHero(GameObject pRes, Character pHero, Weapon.WeaponType eWeaponType = Weapon.WeaponType.Default, Armor.ArmorType eArmorType = Armor.ArmorType.Default)
	{
		GameObject pVisualHero = GameObject.Instantiate(pRes) as GameObject;
		VisualCharacter pVisualChar = pVisualHero.GetComponent<VisualCharacter>();

		if (pVisualChar != null)
		{
			if (eWeaponType != Weapon.WeaponType.Default)
			{
				GameObject pWeaponRef = null;

				switch (eWeaponType)
				{
				case Weapon.WeaponType.Sword:
					pHero.WeaponEquiped = new Sword(pHero);
					pWeaponRef = Sword.Ref;
					break;
				case Weapon.WeaponType.Bow:
					pHero.WeaponEquiped = new Bow(pHero);
					pWeaponRef = Bow.Ref;
					break;
				case Weapon.WeaponType.Staff:
					pHero.WeaponEquiped = new Staff(pHero);
					pWeaponRef = Staff.Ref;
					break;
				}

				if (pWeaponRef != null)
				{
					GameObject pWeaponObject = GameObject.Instantiate(pWeaponRef) as GameObject;
					pWeaponObject.transform.parent = pVisualHero.transform;
					pWeaponObject.transform.localPosition = pWeaponRef.transform.position;
				}
			}

			if (eArmorType != Armor.ArmorType.Default)
				pHero.ArmorEquiped = new Armor(pHero, eArmorType);


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
