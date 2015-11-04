using UnityEngine;
using System.Collections.Generic;

public abstract class ManaPanel : MonoBehaviour
{
#region Variables (public)

	public string m_pOpenButton = null;

	#endregion

#region Variables (private)

	static private List<VisualCharacter> s_pCharacters = null;

	private int m_iCurrentPage = 0;
	private bool m_bOpened = false;
	
	#endregion

	
	virtual public void Initialize()
	{
		s_pCharacters = GameManager.Inst.CharManager.VisualCharactersList;
	}

	public void CatchOpenInput()
	{
		if (Input.GetButtonDown(m_pOpenButton))
		{
			m_bOpened = !m_bOpened;
			GameManager.Inst.UIManager.TogglePanel(this, m_bOpened);
		}
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


	protected VisualCharacter CurrentChar
	{
		get { return s_pCharacters[m_iCurrentPage]; }
	}

	public bool UIElementActive
	{
		set { gameObject.SetActive(value); }
		get { return gameObject.activeSelf; }
	}
}
