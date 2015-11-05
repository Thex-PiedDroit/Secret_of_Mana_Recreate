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

	private bool m_bUIOpened = false;
	
	#endregion


	public UIManager()
	{
		pCharPanel = GameObject.FindObjectOfType<CharacterPanel>();
		//pCanvas = GameObject.FindObjectOfType<Canvas>().gameObject.transform;
		//CharacterPanel pRef = Resources.Load<CharacterPanel>("HUD/CharacPanel");
		//pCharPanel = GameObject.Instantiate(pRef);
		//pCharPanel.gameObject.name = pRef.gameObject.name;
		//pCharPanel.gameObject.transform.SetParent(pCanvas);
		//pCharPanel.transform.localScale = Vector3.one;
		//pCharPanel.transform.localPosition = Vector3.zero;
		//pCharPanel.GetComponent<RawImage>().rectTransform.offsetMin = Vector2.zero;
		//pCharPanel.GetComponent<RawImage>().rectTransform.offsetMax = Vector2.zero;	// Stretch black background to full screen
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
		pPanel.Refresh();
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

		if (bStateChanged)
			OnMenuOpenClose();
	}

	public bool UIOpened
	{
		get { return m_bUIOpened; }
	}
}
