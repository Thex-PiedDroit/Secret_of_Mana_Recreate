using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManager
{
	#region Variables (private)

	private Transform pCanvas = null;
	private CharacterPanel pCharPanel = null;

	public Action OnMenuOpenClose;
	private bool m_bUIOpened = false;
	
	#endregion


	public UIManager()
	{
		pCanvas = GameObject.FindObjectOfType<Canvas>().gameObject.transform;
		CharacterPanel pRef = Resources.Load<CharacterPanel>("HUD/CharacPanel");
		pCharPanel = GameObject.Instantiate(pRef);
		pCharPanel.gameObject.name = pRef.gameObject.name;
		pCharPanel.gameObject.transform.SetParent(pCanvas);
		pCharPanel.transform.localScale = Vector3.one;
		pCharPanel.transform.localPosition = Vector3.zero;
		pCharPanel.GetComponent<RawImage>().rectTransform.offsetMin = Vector2.zero;
		pCharPanel.GetComponent<RawImage>().rectTransform.offsetMax = Vector2.zero;	// Stretch black background to full screen
		pCharPanel.Initialize();
		pCharPanel.Refresh();
		TogglePanel(pCharPanel, false);
	}

	public void Update()
	{
		pCharPanel.CatchOpenInput();
	}
	
	public void TogglePanel(ManaPanel pPanel, bool bOpened)
	{
		// Disable other panels here
		pPanel.UIElementActive = bOpened;	// Currently for tests purposes, will change when more panels to manage
		
		bool bStateChanged = (m_bUIOpened != bOpened);	// this too
		m_bUIOpened = bOpened;

		if (bStateChanged)
			OnMenuOpenClose();
	}

	public bool UIOpened
	{
		get { return m_bUIOpened; }
	}
}
