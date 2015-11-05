using UnityEngine;
using System.Collections.Generic;

public abstract class ManaPanel : MonoBehaviour
{
#region Variables (public)

	public string m_pOpenButton = null;

	#endregion

#region Variables (protected)

	protected int m_iCurrentPage = 0;
	static protected int s_iSelectedCharPage = 0;

	#endregion

#region Variables (private)

	static private List<VisualCharacter> s_pCharacters = null;

	private bool m_bOpened = false;
	
	#endregion

	
	static public void S_Initialize()
	{
		s_pCharacters = GameManager.Inst.CharManager.VisualCharactersList;
	}

	virtual public void Initialize() { }

	virtual public void CatchOpenInput()
	{
		if (Input.GetButtonDown(m_pOpenButton))
		{
			OpenClose();
		}

		if (m_bOpened)
		{
			if (Input.GetButtonDown("Cancel"))
				OpenClose();
			else if (Input.GetButtonDown("Right"))
				ChangePage(true);
			else if (Input.GetButtonDown("Left"))
				ChangePage(false);
		}
	}

	void OpenClose()
	{
		m_iCurrentPage = s_iSelectedCharPage;
		Refresh();
		m_bOpened = !UIElementActive;
		GameManager.Inst.UIManager.TogglePanel(this, m_bOpened);
	}

	public void ChangePage(bool bNext)
	{
		int iChange = bNext ? 1 : -1;

		m_iCurrentPage += iChange;

		if (m_iCurrentPage < 0)
			m_iCurrentPage += s_pCharacters.Count;
		else
			m_iCurrentPage %= s_pCharacters.Count;

		Refresh();
	}

	abstract public void Refresh();


#region Getters/Setters

	protected VisualCharacter CurrentChar
	{
		get { return s_pCharacters[m_iCurrentPage]; }
	}

	public bool UIElementActive
	{
		set { gameObject.SetActive(value); }
		get { return gameObject.activeSelf; }
	}

	#endregion Getters/Setters
}
