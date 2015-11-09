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
	protected Rigidbody m_pRigidBody = null;
	[SerializeField]
	protected Renderer m_pRenderer = null;

	[SerializeField]
	private FloatingDamage pFloatingDamageRef = null;
	[SerializeField]
	private float m_fRagdollThrowForce = 10.0f;
	
	#endregion

#region Variables (protected)

	protected GameObject m_pNPCHealthBar = null;

	protected Vector3 tNavMeshAgentVelocityAtPause = Vector3.zero;
	protected Vector3 tNavMeshAgentDestAtPause = Vector3.zero;
	protected bool m_bPause = false;

	#endregion

#region Variables (private)

	private Character m_pCharacterBUS = null;

	private Animation m_pAttackAnim;

	static private GameObject s_pNPCHealthBarRef = null;
	static private Transform s_pNPCHealthBarsContainer = null;
	static private Transform s_pDamagesTextContainer = null;
	
	#endregion

	static public void S_Initialize()
	{
		s_pNPCHealthBarRef = Resources.Load<GameObject>("HUD/NPC_HealthBar");
		s_pNPCHealthBarsContainer = GameObject.Find("NPC_HealthBars").transform;
		s_pDamagesTextContainer = GameObject.Find("Damages").transform;
	}

	//MonoBehaviour object can't have constructors, you need to create Init method instead
	virtual public void Initialize(Character pCharacter)
	{
		m_pNPCHealthBar = Instantiate(s_pNPCHealthBarRef) as GameObject;
		m_pNPCHealthBar.transform.SetParent(s_pNPCHealthBarsContainer);
		m_pNPCHealthBar.name = s_pNPCHealthBarRef.name;
		m_pNPCHealthBar.SetActive(false);

		//VisualCharacter shouldn't create the logical one
		//Character should be created in the CharacterManager and the VisualCharacter should either be created in same place (kept in separate lists) or in the GameManager
		m_pAttackAnim = GetComponentInChildren<Animation>();

		gameObject.name = pCharacter.Name;
		m_pCharacterBUS = pCharacter;
		m_pCharacterBUS.OnAttack += AttackAnim; //this is how you register for an event
		m_pCharacterBUS.OnHealthChanged += CreateFloatingDamage;
		m_pCharacterBUS.OnDeath += UpdateNPCHealthBar;
		m_pCharacterBUS.OnDeath += EnableRagdoll;
		m_pCharacterBUS.OnDeath += ThrowRagdoll;
		transform.position = m_pCharacterBUS.Position;
	}

	void EnableRagdoll()
	{
		m_pNavMeshAgent.enabled = false;

		if (m_pAttackAnim != null)
			m_pAttackAnim.Stop();

		m_pRigidBody.useGravity = true;
		m_pRigidBody.constraints = RigidbodyConstraints.None;
	}

	void ThrowRagdoll()
	{
		float x = UnityEngine.Random.Range(-0.5f, 0.5f);
		float z = UnityEngine.Random.Range(-0.5f, 0.5f);
		Vector3 tThrowDir = new Vector3(x, 1.0f, z).normalized;
		
		m_pRigidBody.velocity += (tThrowDir * m_fRagdollThrowForce);
		float fRotateSpeed = Random.Range(1.0f, 5.0f);
		m_pRigidBody.angularVelocity += (Random.insideUnitSphere * fRotateSpeed);
	}
	
	virtual public void OnDestroy()
	{
		m_pCharacterBUS.OnAttack -= AttackAnim; //unregister on destroy to avoid any null exceptions
		m_pCharacterBUS.OnHealthChanged -= CreateFloatingDamage;
		m_pCharacterBUS.OnDeath -= UpdateNPCHealthBar;
		m_pCharacterBUS.OnDeath -= EnableRagdoll;
		m_pCharacterBUS.OnDeath -= ThrowRagdoll;
	}

	virtual public void Update()
	{
		if (!m_bPause && m_pCharacterBUS.IsAlive)
		{
			if (m_pCharacterBUS.Destination != m_pNavMeshAgent.destination)
				m_pNavMeshAgent.SetDestination(m_pCharacterBUS.Destination);

			if (m_pNavMeshAgent.hasPath)
				m_pCharacterBUS.Forward = transform.forward;
			else
				transform.forward = m_pCharacterBUS.Forward;

			m_pCharacterBUS.Position = transform.position;

			UpdateNPCHealthBar();
		}

		else if (!m_bPause && m_pCharacterBUS.DeadHit)
		{
			ThrowRagdoll();
			m_pCharacterBUS.DeadHit = false;
		}
	}

	protected void UpdateNPCHealthBar()
	{
		float fCurrentHP = m_pCharacterBUS.CurrentHP;
		float fHPMax = m_pCharacterBUS.HPMax;

		if (m_pCharacterBUS.IsAlive && m_pRenderer.isVisible && fCurrentHP != fHPMax)
		{
			float fHealthPercent = fCurrentHP / fHPMax;
			Transform pHealthBarRed = m_pNPCHealthBar.transform.GetChild(0);
			pHealthBarRed.localScale = new Vector3(fHealthPercent, 1.0f, 1.0f);

			m_pNPCHealthBar.SetActive(true);
			m_pNPCHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + (Vector3.up * 2.0f));
		}

		else
			m_pNPCHealthBar.SetActive(false);
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
			m_pNPCHealthBar.SetActive(false);
			tNavMeshAgentVelocityAtPause = m_pNavMeshAgent.velocity;
			tNavMeshAgentDestAtPause = m_pNavMeshAgent.destination;

			m_pNavMeshAgent.enabled = false;
		}

		else if (m_pCharacterBUS.IsAlive)
		{
			UpdateNPCHealthBar();
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
