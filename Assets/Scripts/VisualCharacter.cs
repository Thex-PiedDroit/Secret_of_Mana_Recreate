using UnityEngine;
using System.Collections;

public class VisualCharacter : MonoBehaviour
{
	#region Variables (public)

	[SerializeField]
	private string m_pName = null;

	[SerializeField]
	private Animation m_pAttackAnim;
	[SerializeField]
	private NavMeshAgent m_pNavMeshAgent;
	
	#endregion
	
	#region Variables (private)

	private Character m_pCharacterBUS = null;
	
	#endregion

    //MonoBehaviour object can't have constructors, you need to create Init method instead
	public void Initialise(Character character)
	{
        //VisualCharacter shouldn't create the logical one
        //Character should be created in the CharacterManager and the VisualCharacter should either be created in same place (kept in separate lists) or in the GameManager
        //m_pCharacterBUS = new Character(m_pName, this);	// I'm not sure how else i could send a custom name to the Character without asking in game

        m_pCharacterBUS = character;
        m_pCharacterBUS.OnHitTaken += AttackAnim; //this is how you register for an event
        transform.position = m_pCharacterBUS.Position;
    }
	
    void OnDestroy()
    {
        m_pCharacterBUS.OnHitTaken -= AttackAnim; //unregister on destroy to avoid any null exceptions
    }
    void Update()
    {
        transform.forward = m_pCharacterBUS.Forward;
        m_pNavMeshAgent.SetDestination(m_pCharacterBUS.Destination);
        if (m_pCharacterBUS.Selected)
        {
            transform.position = m_pCharacterBUS.Position;
        }
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

	public Vector3 Pos
	{
		get { return transform.position; }
	}

	public Vector3 Destination
	{
		set { m_pNavMeshAgent.SetDestination(value); }
	}

	public bool HasPath
	{
		get { return m_pNavMeshAgent.hasPath; }
	}

	public Character CharacterBUS
	{
		get { return m_pCharacterBUS; }
	}
}
