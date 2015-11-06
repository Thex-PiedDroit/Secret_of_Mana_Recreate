using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class InventoryPanel : ManaPanel
{
	#region Variables (public)

	[SerializeField]
	private Transform pInvContainer = null;

	[SerializeField]
	private Vector2 m_tFirstSlotPos = new Vector2(80.0f, -85.0f);
	[SerializeField]
	private Vector2 m_tSpaceBetweenSlots = new Vector2(154.0f, -161.0f);
	[SerializeField]
	private int m_iColumnsCount = 5;
	
	#endregion
	
	#region Variables (private)
	
	private List<List<GameObject>> pItemsLists = new List<List<GameObject>>(3);
	
	#endregion


	override public void Initialize()
	{
		for (int i = 0; i < pItemsLists.Capacity; i++)
			pItemsLists.Add(new List<GameObject>());
	}

	void CreateItem(GameObject pItemRef, int iCharID)
	{
		Assert.IsTrue((iCharID > 0) && (iCharID < pItemsLists.Capacity));

		int i = pItemsLists.Count;
		int x = i % m_iColumnsCount;
		int y = i / m_iColumnsCount;
		Vector2 tOffset = m_tSpaceBetweenSlots;
		tOffset.Scale(new Vector2(x, y));
		Vector3 tPos = m_tFirstSlotPos + tOffset;

		pItemsLists[iCharID].Add(Instantiate(pItemRef, Vector3.zero, Quaternion.identity) as GameObject);
		pItemsLists[iCharID][i].transform.SetParent(pInvContainer);
		pItemsLists[iCharID][i].transform.localPosition = tPos;
		pItemsLists[iCharID][i].transform.localScale = Vector3.one;
	}

	public override void Refresh()
	{
		
	}
}
