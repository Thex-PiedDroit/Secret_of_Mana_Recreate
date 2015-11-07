using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class VisualCharacter : MonoBehaviour
{
#region Variables (public)

	[SerializeField]
	private NavMeshAgent m_pNavMeshAgent = null;

	[SerializeField]
	private FloatingDamage pFloatingDamageRef = null;
	
	#endregion
	
#region Variables (private)

	private Character m_pCharacterBUS = null;

	private Animation m_pAttackAnim;

	private Vector3 tNavMeshAgentVelocityAtPause = Vector3.zero;
	private Vector3 tNavMeshAgentDestAtPause = Vector3.zero;
	private bool m_bPause = false;

	static private Transform s_pDamagesTextContainer = null;
	static private Transform s_pHealthBar = null;
	static private Text s_pHealthBarValue = null;
	
	#endregion

	//MonoBehaviour object can't have constructors, you need to create Init method instead
	public void Initialize(Character pCharacter)
	{
		s_pDamagesTextContainer = GameObject.Find("Damages").transform;
		s_pHealthBar = GameObject.Find("HealthBar").transform;
		s_pHealthBarValue = GameObject.Find("HealthBar_Value").GetComponent<Text>();

		//VisualCharacter shouldn't create the logical one
		//Character should be created in the CharacterManager and the VisualCharacter should either be created in same place (kept in separate lists) or in the GameManager
		m_pAttackAnim = GetComponentInChildren<Animation>();

		gameObject.name = pCharacter.Name;
		m_pCharacterBUS = pCharacter;
		m_pCharacterBUS.OnHitTaken += AttackAnim; //this is how you register for an event
		m_pCharacterBUS.OnHealthChanged += UpdateHealthBar;
		m_pCharacterBUS.OnHealthChanged += CreateFloatingDamage;
		transform.position = m_pCharacterBUS.Position;
	}
	
	void OnDestroy()
	{
		m_pCharacterBUS.OnHitTaken -= AttackAnim; //unregister on destroy to avoid any null exceptions
		m_pCharacterBUS.OnHealthChanged -= UpdateHealthBar;
		m_pCharacterBUS.OnHealthChanged -= CreateFloatingDamage;
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

			else	// Keep logic Character's data up to date if navMeshAgent driven
			{
				m_pNavMeshAgent.SetDestination(m_pCharacterBUS.Destination);
				m_pCharacterBUS.Forward = transform.forward;
				m_pCharacterBUS.Position = transform.position;
			}
		}
	}

	void CreateFloatingDamage()
	{
		Vector3 tPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
		FloatingDamage pFloatingDamage = Instantiate(pFloatingDamageRef, tPos, Quaternion.identity) as FloatingDamage;
		pFloatingDamage.transform.SetParent(s_pDamagesTextContainer);
		pFloatingDamage.ValueToText = m_pCharacterBUS.LastHPChange;
	}

	void UpdateHealthBar()
	{
		if (m_pCharacterBUS.Selected)
		{
			float fCurrentHP = m_pCharacterBUS.CurrentHP;
			float fHPMax = m_pCharacterBUS.HPMax;

			float fHealthPercent = fCurrentHP / fHPMax;
			s_pHealthBar.localScale = new Vector3(fHealthPercent, 1.0f, 1.0f);

			s_pHealthBarValue.text = string.Format("{0}/{1}", fCurrentHP, fHPMax);
		}
	}

	public void ToggleSelect()
	{
		m_pCharacterBUS.Selected = !m_pCharacterBUS.Selected;

		if (m_pCharacterBUS.Selected)
		{
			tNavMeshAgentDestAtPause = transform.position;
			tNavMeshAgentVelocityAtPause = Vector3.zero;

			if (m_pNavMeshAgent.enabled)
				m_pNavMeshAgent.SetDestination(transform.position);
		}

		UpdateHealthBar();
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

	public List<Item> Inv
	{
		get { return m_pCharacterBUS.Inv.Items; }
	}

	#endregion Getters/Setters
}
