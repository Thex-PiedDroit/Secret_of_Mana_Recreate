using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
	#region Variables (public)
	
	[SerializeField]
	private Vector3 m_tOffset = new Vector3(0.0f, 10.0f, -10.0f);
	[SerializeField]
	private float m_fFollowSpeed = 10.0f;
	
	#endregion
	
	#region Variables (private)

	private CharacterManager pCharacManager;
	
	#endregion


	void Start()
	{
		pCharacManager = CharacterManager.Inst;
	}

	void LateUpdate()
	{
		Vector3 tHeroPos = pCharacManager.SelectedHeroPos;
		transform.position = Vector3.Lerp(transform.position, tHeroPos + m_tOffset, m_fFollowSpeed * Time.deltaTime);
	}
}
