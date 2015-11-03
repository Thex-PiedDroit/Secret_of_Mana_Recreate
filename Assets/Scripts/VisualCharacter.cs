using UnityEngine;
using System.Collections;

public class VisualCharacter : MonoBehaviour
{
	#region Variables (public)

	[SerializeField]
	private string m_pName = null;

	[SerializeField]
	private Animation m_pAttackAnim;
	
	#endregion
	
	#region Variables (private)

	private Character m_pCharacter = null;
	
	#endregion


	void Start()
	{
		m_pCharacter = new Character(m_pName, this);	// I'm not sure how else i could send a custom name to the Character
	}

	void FixedUpdate()
	{
		m_pCharacter.FixedUpdate();
	}
	
	void Update()
	{
		m_pCharacter.Update();
	}

	public void Attack()
	{
		m_pAttackAnim.Play();
	}


	public Vector3 Forward
	{
		set { transform.forward = value; }
		get { return transform.forward; }
	}
}
