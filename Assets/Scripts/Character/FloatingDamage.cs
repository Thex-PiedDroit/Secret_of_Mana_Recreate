using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingDamage : MonoBehaviour
{
#region Variables (public)

	[SerializeField]
	private Text pText = null;
	[SerializeField]
	private Rigidbody2D pRigidBody = null;
	[SerializeField]
	private float m_fInitialVelocity = 80.0f;
	[SerializeField]
	private float m_fLifeTime = 1.0f;
	[SerializeField]
	private float m_fAlphaCurveExponent = 3.0f;
	
	#endregion
	
#region Variables (private)

	private float m_fSpawnTime = 0.0f;
	
	#endregion


	void Start()
	{
		Vector2 tInitDir = new Vector2(Random.Range(-1.0f, 1.0f), 1.0f).normalized;
		pRigidBody.velocity += (tInitDir * (m_fInitialVelocity * m_fAlphaCurveExponent));
		m_fSpawnTime = Time.fixedTime;
	}
	
	void Update()
	{
		float fLivedTime = Time.fixedTime - m_fSpawnTime;

		if (fLivedTime < m_fLifeTime)
		{
			float fLivedPercent = fLivedTime / m_fLifeTime;

			float fAlpha = 1.0f - (Mathf.Pow(fLivedPercent, 3.0f));
			if (fAlpha < 0.0f)
				fAlpha = 0.0f;
			Color tColor = pText.color;
			tColor.a = fAlpha;
			pText.color = tColor;

			pRigidBody.velocity += Physics2D.gravity;
		}

		else
			Destroy(gameObject);
	}


	public int ValueToText
	{
		set
		{
			if (value > 0)
			{
				pText.color = Color.green;
				pText.text = string.Format("+{0}", value);

				return;
			}

			else if (value == 0)
				pText.color = Color.white;

			else
				pText.color = Color.red;

			pText.text = value.ToString();
		}
	}
}
