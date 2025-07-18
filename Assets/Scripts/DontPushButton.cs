using UnityEngine;
using UnityEngine.Serialization;

public class DontPushButton : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	[FormerlySerializedAs("ButtonPart")]
	public Transform buttonPart;

	public DuckSpawner duckSpawner;

	private bool m_onButton;
	private float m_pressTime;
	private Vector3 m_startPosition;

    void Start()
    {
		m_onButton = false;
		m_pressTime = 0;
		m_startPosition = buttonPart.localPosition;
	}

    // Update is called once per frame
    void Update()
    {
		Vector3 newPosition = m_startPosition - Vector3.up * 0.05f * m_pressTime;
		buttonPart.localPosition = newPosition;

		if (m_onButton)
		{
			m_pressTime += Time.deltaTime * 3;
		}
		else
		{
			m_pressTime -= Time.deltaTime * 3;
		}

		m_pressTime = Mathf.Clamp(m_pressTime, 0, 1);
	}

	void FixedUpdate()
	{
		if (m_onButton)
		{
			duckSpawner.Spawn();
		}
	}

			//Upon collision with another GameObject, this GameObject will reverse direction
	private void OnTriggerEnter(Collider other)
	{
		m_onButton = true;
	}

	private void OnTriggerExit(Collider other)
	{
		m_onButton = false;
	}
}
