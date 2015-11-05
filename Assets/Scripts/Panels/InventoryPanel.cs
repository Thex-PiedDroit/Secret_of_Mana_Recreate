using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryPanel : ManaPanel
{
	#region Variables (public)

	[SerializeField]
	private Vector2 m_tFirstSlotPos = new Vector2(80.0f, -85.0f);
	[SerializeField]
	private Vector2 m_tSpaceBetweenSlots = new Vector2(154.0f, -161.0f);
	
	#endregion
	
	#region Variables (private)
	
	//private List<GameObject> p
	
	#endregion


	void Start()
	{
		
	}

	public override void Refresh()
	{
		
	}
}
