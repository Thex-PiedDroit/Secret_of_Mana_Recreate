using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManager
{
#region Variables (public)

	public Action OnMenuOpenClose;

	#endregion

#region Variables (private)

	//private Transform pCanvas = null;
	private CharacterPanel pCharPanel = null;
	private InventoryPanel pInvPanel = null;
	private GameObject pHealthBar = null;

	private bool m_bUIOpened = false;
	
	#endregion


	public UIManager()
	{
		pHealthBar = GameObject.Find("HealthBar_Back");

		ManaPanel.S_Initialize();
		pCharPanel = GameObject.FindObjectOfType<CharacterPanel>();
		pInvPanel = GameObject.FindObjectOfType<InventoryPanel>();
		InitPanel(pCharPanel);
		InitPanel(pInvPanel);
	}

	public void Update()
	{
		pCharPanel.CatchOpenInput();
		pInvPanel.CatchOpenInput();
	}

	void InitPanel(ManaPanel pPanel)
	{
		pPanel.Initialize();
		TogglePanel(pPanel, false);
	}
	
	public void TogglePanel(ManaPanel pPanel, bool bOpened)
	{
		pCharPanel.UIElementActive = false;
		pInvPanel.UIElementActive = false;

		pPanel.UIElementActive = bOpened;

		bool bNewState = pCharPanel.UIElementActive || pInvPanel.UIElementActive;
		bool bStateChanged = (m_bUIOpened != bNewState);
		m_bUIOpened = bNewState;
		
		pHealthBar.SetActive(!m_bUIOpened);

		if (bStateChanged)
			OnMenuOpenClose();
	}

	public bool UIOpened
	{
		get { return m_bUIOpened; }
	}
}
