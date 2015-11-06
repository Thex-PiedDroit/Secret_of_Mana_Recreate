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
	
	private List<List<GameObject>> m_pIconsLists = new List<List<GameObject>>(3);
	private List<List<Item>> m_pItemsLists = new List<List<Item>>(3);

	private List<GameObject> m_pCurrentPageIconsList = null;
	private List<Item> m_pCurrentPageItemsList = null;
	
	#endregion


	override public void Initialize()
	{
		for (int i = 0; i < m_pIconsLists.Capacity; i++)
		{
			m_pIconsLists.Add(new List<GameObject>());
			m_pItemsLists.Add(new List<Item>());
		}
	}

	void CreateIcon(GameObject pItemRef)
	{
		List<GameObject> pCurrentPageList = m_pIconsLists[m_iCurrentPage];

		int i = pCurrentPageList.Count;
		Vector3 tPos = GetIconPos(i);

		pCurrentPageList.Add(Instantiate(pItemRef, Vector3.zero, Quaternion.identity) as GameObject);
		pCurrentPageList[i].gameObject.name = pItemRef.name;
		pCurrentPageList[i].transform.SetParent(pInvContainer);
		pCurrentPageList[i].transform.localPosition = tPos;
		pCurrentPageList[i].transform.localScale = Vector3.one;
	}

	void ClearInventory(List<Item> pCharInv)
	{
		List<int> pItemsToDelete = new List<int>();

		int i = 0;

		for (i = 0; i < m_pCurrentPageItemsList.Count; i++)
		{
			if (!pCharInv.Contains(m_pCurrentPageItemsList[i]))
				pItemsToDelete.Add(i);
		}

		for (i = pItemsToDelete.Count - 1; i >= 0; i--)		// Remove backwards to keep indexes accurate
		{
			int iID = pItemsToDelete[i];
			Destroy(m_pCurrentPageIconsList[iID].gameObject);
			m_pCurrentPageIconsList.RemoveAt(iID);
			m_pCurrentPageItemsList.RemoveAt(iID);
		}


		if (pItemsToDelete.Count > 0)
			SortIcons();
	}

	void SortIcons()
	{
		for (int i = 0; i < m_pCurrentPageIconsList.Count; i++)
		{
			Vector3 tPos = GetIconPos(i);
			m_pCurrentPageIconsList[i].transform.localPosition = tPos;
		}
	}

	Vector3 GetIconPos(int iIconIndex)
	{
		int x = iIconIndex % m_iColumnsCount;
		int y = iIconIndex / m_iColumnsCount;
		Vector2 tOffset = m_tSpaceBetweenSlots;
		tOffset.Scale(new Vector2(x, y));
		
		return (m_tFirstSlotPos + tOffset);
	}


	public override void Refresh()
	{
		m_pCurrentPageItemsList = m_pItemsLists[m_iCurrentPage];
		m_pCurrentPageIconsList = m_pIconsLists[m_iCurrentPage];

		VisualCharacter pCurrentChar = CurrentPageChar;
		pName.text = pCurrentChar.Name;

		List<Item> pCurrentCharInv = pCurrentChar.Inv;

		ClearInventory(pCurrentCharInv);	// Clear items that aren't in inventory anymore

		for (int i = 0; i < pCurrentCharInv.Count; i++)
		{
			if (!(m_pCurrentPageItemsList.Contains(pCurrentCharInv[i])))
			{
				m_pCurrentPageItemsList.Add(pCurrentCharInv[i]);
				CreateIcon(pCurrentCharInv[i].IconRef);
			}
		}

		DisplayCurrentPage();
	}

	void DisplayCurrentPage()
	{
		int i = 0;

		/* Disable everything first */

		for (i = 0; i < m_pIconsLists.Capacity; i++)
		{
			List<GameObject> pList = m_pIconsLists[i];

			for (int j = 0; j < pList.Count; j++)
			{
				pList[j].SetActive(false);
			}
		}


		/* Now activate current page */

		for (i = 0; i < m_pCurrentPageIconsList.Count; i++)
			m_pCurrentPageIconsList[i].SetActive(true);
	}
}
