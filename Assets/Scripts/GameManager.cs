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
	private UIManager m_pUIManager = null;
	
	#endregion

	public CharacterManager CharManager
	{
		get
		{
			return m_pCharacManager;
		}
	}

	public UIManager UIManager
	{
		get { return m_pUIManager; }
	}

	static public GameManager Inst
	{
		get { return s_pInst; }
	}
	
	void Start()
	{
		s_pInst = this;

		m_pCharacManager = new CharacterManager(); // doesn't need to be singleton
		m_pUIManager = new UIManager();
		m_pUIManager.OnMenuOpenClose += m_pCharacManager.TogglePause;
	}

	void Update()
	{
		m_pUIManager.Update();
		m_pCharacManager.Update();
	}
}
