using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterManager
{
#region Variables (private)

	static private CharacterManager s_pInst = null;

	private List<VisualCharacter> m_pHeroesPRES = null;
    private Character _hero1 = null;
    private Character _hero2 = null;
    private Character _hero3 = null;
    private Character m_selectedHero = null; 
	
	#endregion
    

	public CharacterManager()
	{
        _hero1 = new Character("char1", Vector3.zero);
        _hero2 = new Character("char2", Vector3.one);
        _hero3 = new Character("char3", -Vector3.one);
        m_pHeroesPRES = new List<VisualCharacter>();

        GameObject res = Resources.Load("Heroes/Lucia") as GameObject;
        CreateHeroes(res, _hero1);
        CreateHeroes(res, _hero2);
        CreateHeroes(res, _hero3);

        m_selectedHero = _hero1;
        _hero1.Selected = true;
    }

    private void CreateHeroes(GameObject res, Character hero)
    {
        GameObject visualHero = GameObject.Instantiate(res) as GameObject;
        VisualCharacter visualChar = visualHero.GetComponent<VisualCharacter>();
        if (visualChar != null)
        {
            visualChar.Initialise(hero);
            m_pHeroesPRES.Add(visualChar);
        }
    }

    public void Update()
    {
        m_selectedHero.CatchInputs();
        _hero1.FollowSelected(m_selectedHero);
        _hero2.FollowSelected(m_selectedHero);
        _hero3.FollowSelected(m_selectedHero);


        /*
		for (int i = 0; i < m_pHeroesBUS.Length; i++)
		{
			if (i == m_iSelectedHero)
				m_pHeroesBUS[i].CatchInputs();
			else
				m_pHeroesBUS[i].FollowSelected(m_pHeroesPRES[m_iSelectedHero].Pos);
		}
        /**/
    }

	public Character SelectedHero
	{
		get { return m_selectedHero; }
	}
}
