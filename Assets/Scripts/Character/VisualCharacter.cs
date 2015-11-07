using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VisualCharacter : MonoBehaviour
{
#region Variables (public)

	[SerializeField]
	protected NavMeshAgent m_pNavMeshAgent = null;

	[SerializeField]
	private FloatingDamage pFloatingDamageRef = null;
	
	#endregion

#region Variables (protected)

	protected Vector3 tNavMeshAgentVelocityAtPause = Vector3.zero;
	protected Vector3 tNavMeshAgentDestAtPause = Vector3.zero;
	protected bool m_bPause = false;

	#endregion

#region Variables (private)

	private Character m_pCharacterBUS = null;

	private Animation m_pAttackAnim;

	static private Transform s_pDamagesTextContainer = null;
	
	#endregion

	//MonoBehaviour object can't have constructors, you need to create Init method instead
	virtual public void Initialize(Character pCharacter)
	{
		s_pDamagesTextContainer = GameObject.Find("Damages").transform;

		//VisualCharacter shouldn't create the logical one
		//Character should be created in the CharacterManager and the VisualCharacter should either be created in same place (kept in separate lists) or in the GameManager
		m_pAttackAnim = GetComponentInChildren<Animation>();

		gameObject.name = pCharacter.Name;
		m_pCharacterBUS = pCharacter;
		m_pCharacterBUS.OnAttack += AttackAnim; //this is how you register for an event
		m_pCharacterBUS.OnHealthChanged += CreateFloatingDamage;
		transform.position = m_pCharacterBUS.Position;
	}
	
	virtual public void OnDestroy()
	{
		m_pCharacterBUS.OnAttack -= AttackAnim; //unregister on destroy to avoid any null exceptions
		m_pCharacterBUS.OnHealthChanged -= CreateFloatingDamage;
	}

	void Update()
	{
		if (!m_bPause)
		{
			if (m_pCharacterBUS.Destination != m_pNavMeshAgent.destination)
				m_pNavMeshAgent.SetDestination(m_pCharacterBUS.Destination);
			m_pCharacterBUS.Forward = transform.forward;
			m_pCharacterBUS.Position = transform.position;
		}
	}

	void CreateFloatingDamage()
	{
		Vector3 tPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
		FloatingDamage pFloatingDamage = Instantiate(pFloatingDamageRef, tPos, Quaternion.identity) as FloatingDamage;
		pFloatingDamage.transform.SetParent(s_pDamagesTextContainer);
		pFloatingDamage.ValueToText = m_pCharacterBUS.LastHPChange;
	}


#region CallBacks

	public void AttackAnim()
	{
		m_pAttackAnim.Rewind();
		m_pAttackAnim.Play();
	}

	public void TogglePause()
	{
		m_bPause = !m_bPause;

		if (m_bPause)
		{
			tNavMeshAgentVelocityAtPause = m_pNavMeshAgent.velocity;
			tNavMeshAgentDestAtPause = m_pNavMeshAgent.destination;

			m_pNavMeshAgent.enabled = false;
		}

		else
		{
			m_pNavMeshAgent.enabled = true;

			m_pNavMeshAgent.destination = tNavMeshAgentDestAtPause;
			m_pNavMeshAgent.velocity = tNavMeshAgentVelocityAtPause;
		}
	}

	#endregion CallBacks


#region Getters/Setters

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

	public List<Item> Inv
	{
		get { return m_pCharacterBUS.Inv.Items; }
	}

	#endregion Getters/Setters
}
