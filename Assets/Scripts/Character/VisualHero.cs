using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VisualHero : VisualCharacter
{
#region Variables (private)
	
	private Hero m_pHeroBUS = null;

	static private Transform s_pHealthBar = null;
	static private Text s_pHealthBarValue = null;
	
	#endregion


	override public void Initialize(Character pHero)
	{
		m_pHeroBUS = pHero as Hero;
		base.Initialize(pHero);
		s_pHealthBar = GameObject.Find("HealthBar").transform;
		s_pHealthBarValue = GameObject.Find("HealthBar_Value").GetComponent<Text>();
		m_pHeroBUS.OnHealthChanged += UpdateHealthBar;

		UpdateHealthBar();
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		m_pHeroBUS.OnHealthChanged -= UpdateHealthBar;
	}
	
	override public void Update()
	{
		if (!m_bPause && m_pHeroBUS.IsAlive)
		{
			if (m_pHeroBUS.Selected)
			{
				transform.forward = m_pHeroBUS.Forward;

				NavMeshHit Hit;
				NavMesh.SamplePosition(m_pHeroBUS.Position, out Hit, float.MaxValue, 1);
				transform.position = Hit.position;
				m_pHeroBUS.Position = Hit.position;		// Keep character within NavMesh walkable area
			}

			else	// Keep logic Character's data up to date if navMeshAgent driven
			{
				base.Update();
			}
		}
	}

	void UpdateHealthBar()
	{
		if (m_pHeroBUS.Selected)
		{
			float fCurrentHP = m_pHeroBUS.CurrentHP;
			float fHPMax = m_pHeroBUS.HPMax;

			float fHealthPercent = fCurrentHP / fHPMax;
			s_pHealthBar.localScale = new Vector3(fHealthPercent, 1.0f, 1.0f);

			s_pHealthBarValue.text = string.Format("{0}/{1}", fCurrentHP, fHPMax);
		}
	}

	public void ToggleSelect()
	{
		m_pHeroBUS.Selected = !m_pHeroBUS.Selected;

		if (m_pHeroBUS.Selected)
		{
			m_pNPCHealthBar.SetActive(false);

			tNavMeshAgentDestAtPause = transform.position;
			tNavMeshAgentVelocityAtPause = Vector3.zero;

			if (m_pNavMeshAgent.enabled)
				m_pNavMeshAgent.SetDestination(transform.position);
		}

		UpdateHealthBar();
	}


#region Getters/Setters
#region UITexts
	public string Name
	{
		get { return m_pHeroBUS.Name; }
	}

	public string LevelText
	{
		get { return m_pHeroBUS.Level.ToString(); }
	}

	public string HPText
	{
		get { return string.Format("{0}/{1}", m_pHeroBUS.CurrentHP, m_pHeroBUS.HPMax); }
	}

	public string MPText
	{
		get { return string.Format("{0}/{1}", m_pHeroBUS.CurrentMP, m_pHeroBUS.MPMax); }
	}

	public string AtkText
	{
		get { return m_pHeroBUS.Atk.ToString() /* - pWeapon.Atk*/; }
	}

	public string AtkBonusText
	{
		get { return string.Format("+{0}", m_pHeroBUS.WpnAtk) /* - pWeapon.Atk*/; }
	}

	public string DefText
	{
		get { return m_pHeroBUS.Def.ToString() /* - pArmor.Def*/; }
	}

	public string DefBonusText
	{
		get { return string.Format("+{0}", m_pHeroBUS.ArmorDef) /* - pWeapon.Atk*/; }
	}
	#endregion UITexts

	public Hero HeroBUS
	{
		get { return m_pHeroBUS; }
	}

	#endregion Getters/Setters
}
