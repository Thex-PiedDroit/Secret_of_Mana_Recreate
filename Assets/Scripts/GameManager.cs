using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	#region Variables (public)

	
	
	#endregion
	
	#region Variables (private)

	static private GameManager s_pInst = null;

	private CharacterManager m_pCharacManager = null;
	
	#endregion

    public CharacterManager CharManager
    {
        get
        {
            return m_pCharacManager;
        }
    }
	static public GameManager Inst
	{
		get { return s_pInst; }
	}
	
	void Start()
	{
		s_pInst = this;

		m_pCharacManager = new CharacterManager(); // doesn't need to be singleton
		CreateHeroes();
	}

	void Update()
	{
		m_pCharacManager.Update();
	}


	public void CreateHeroes()
	{
        /*
		VisualCharacter[] pCharactersPRES = m_pCharacManager.HeroesPRES;
		Character[] pCharactersBUS = m_pCharacManager.HeroesBUS;
		for (int i = 0; i < pCharactersPRES.Length; i++)
		{
			pCharactersPRES[i] = Instantiate(pCharactersPRES[i], Vector3.back * i, Quaternion.identity) as VisualCharacter;
			pCharactersBUS[i] = pCharactersPRES[i].CharacterBUS;
		}
        /**/
	}
}
