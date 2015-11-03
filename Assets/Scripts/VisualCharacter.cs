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

	private Character m_pCharacterBUS = null;
	
	#endregion


	public VisualCharacter()
	{
		m_pCharacterBUS = new Character(m_pName, this);	// I'm not sure how else i could send a custom name to the Character without asking in game
	}
	

	public void AttackAnim()
	{
		m_pAttackAnim.Play();
	}


	public Vector3 Forward
	{
		set { transform.forward = value; }
		get { return transform.forward; }
	}

	public Character CharacterBUS
	{
		get { return m_pCharacterBUS; }
	}
}
