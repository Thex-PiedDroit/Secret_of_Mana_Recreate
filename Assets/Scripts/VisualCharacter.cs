using UnityEngine;
using System.Collections;

public class VisualCharacter : MonoBehaviour
{
	#region Variables (public)

	[SerializeField]
	private Animation m_pAttackAnim;
	[SerializeField]
	private NavMeshAgent m_pNavMeshAgent;
	
	#endregion
	
	#region Variables (private)

	private Character m_pCharacterBUS = null;

	private Vector3 tNavMeshAgentVelocityAtPause = Vector3.zero;
	private Vector3 tNavMeshAgentDestAtPause = Vector3.zero;
	private bool m_bPause = false;
	
	#endregion

	//MonoBehaviour object can't have constructors, you need to create Init method instead
	public void Initialize(Character pCharacter)
	{
		//VisualCharacter shouldn't create the logical one
		//Character should be created in the CharacterManager and the VisualCharacter should either be created in same place (kept in separate lists) or in the GameManager

		gameObject.name = pCharacter.Name;
		m_pCharacterBUS = pCharacter;
		m_pCharacterBUS.OnHitTaken += AttackAnim; //this is how you register for an event
		transform.position = m_pCharacterBUS.Position;
	}
	
	void OnDestroy()
	{
		m_pCharacterBUS.OnHitTaken -= AttackAnim; //unregister on destroy to avoid any null exceptions
	}

	void Update()
	{
		if (!m_bPause)
		{
			if (m_pCharacterBUS.Selected)
			{
				transform.forward = m_pCharacterBUS.Forward;
				transform.position = m_pCharacterBUS.Position;
			}

			else
			{
				m_pNavMeshAgent.SetDestination(m_pCharacterBUS.Destination);
				m_pCharacterBUS.Forward = transform.forward;
				m_pCharacterBUS.Position = transform.position;
			}
		}
	}

	public void AttackAnim()
	{
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


#region Getters/Setters

#region UITexts
	public string Name
	{
		get { return m_pCharacterBUS.Name; }
	}

	public string LevelText
	{
		get { return m_pCharacterBUS.Level.ToString(); }
	}

	public string HPText
	{
		get { return string.Format("{0}/{1}", m_pCharacterBUS.CurrentHP, m_pCharacterBUS.HPMax); }
	}

	public string MPText
	{
		get { return string.Format("{0}/{1}", m_pCharacterBUS.CurrentMP, m_pCharacterBUS.MPMax); }
	}

	public string AtkText
	{
		get { return m_pCharacterBUS.Atk.ToString() /* - pWeapon.Atk*/; }
	}

	public string AtkBonusText
	{
		get { return string.Format("+{0}", m_pCharacterBUS.WpnAtk) /* - pWeapon.Atk*/; }
	}

	public string DefText
	{
		get { return m_pCharacterBUS.Def.ToString() /* - pArmor.Def*/; }
	}

	public string DefBonusText
	{
		get { return string.Format("+{0}", m_pCharacterBUS.ArmorDef) /* - pWeapon.Atk*/; }
	}
	#endregion UITexts

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

	#endregion Getters/Setters
}
