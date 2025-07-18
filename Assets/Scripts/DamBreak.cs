using UnityEngine;
using FluidFrenzy;

public class DamBreak : MonoBehaviour
{
	FluidEventTrigger m_trigger;
	bool m_triggered = false;
	bool m_reachedHeight = false;
	public bool m_damBroken = false;
	float m_breakTime = 100;

	FluidSimulationObstacle[] m_obstacles;
	Rigidbody[] m_rigidBodies;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		m_trigger = GetComponent<FluidEventTrigger>();
		m_obstacles = transform.parent.GetComponentsInChildren<FluidSimulationObstacle>();
		m_rigidBodies = transform.parent.GetComponentsInChildren<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
		if (m_damBroken)
		{
			return;
		}
        if(m_triggered)
		{
			if (!m_reachedHeight)
			{
				m_reachedHeight = m_trigger.fluidHeight > transform.position.y;
				if (m_reachedHeight)
				{
					m_breakTime = 1;
				}
			}
			else
			{
				m_breakTime -= Time.deltaTime;
				m_breakTime = Mathf.Max(0, m_breakTime);
				if (m_breakTime == 0)
				{
					m_breakTime = 1;
					m_damBroken = true;
					StartDamBreak();
				}
			}
		}
		else
		{
			foreach (Rigidbody rigidbody in m_rigidBodies)
			{
				if(rigidbody.IsSleeping())
					rigidbody.isKinematic = true;
				//rigidbody.AddForce(Vector3.back * 1000000 * 0.5f);
			}
		}
    }

	private void FixedUpdate()
	{
		if (m_damBroken)
		{
			m_breakTime -= Time.fixedDeltaTime;
			m_breakTime = Mathf.Max(0, m_breakTime);

			if (m_breakTime > 0)
			{
				foreach (Rigidbody rigidbody in m_rigidBodies)
				{
					rigidbody.isKinematic = false;
					rigidbody.AddForce(Vector3.back * 25.0f * 0.30f, ForceMode.Acceleration);
					rigidbody.AddForce(Vector3.up * 2.5f * 0.30f, ForceMode.Acceleration);
				}
			}
			return;
		}
	}

	private void StartDamBreak()
	{
		foreach(FluidSimulationObstacle obstacle in m_obstacles)
		{
			obstacle.enabled = false;
		}


	}

	public void OnFluidEnter(FluidEventTrigger trigger)
	{
		m_triggered = true;
	}
}
