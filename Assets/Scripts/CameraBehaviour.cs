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
		pCharacManager = GameManager.Inst.CharManager;
	}

	void LateUpdate()
	{
        Character hero = pCharacManager.SelectedHero;
        if (hero != null)
        {
            transform.position = Vector3.Lerp(transform.position, hero.Position + m_tOffset, m_fFollowSpeed * Time.deltaTime);
        }
	}
}
