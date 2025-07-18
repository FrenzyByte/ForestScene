using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public GameObject Duck;

	private GameObject[] m_ducks;

	private int m_activeDucks = 0;

    void Start()
    {
		m_ducks = new GameObject[500];

		for(int i = 0; i < m_ducks.Length; i++)
		{
			m_ducks[i] = GameObject.Instantiate(Duck);
			m_ducks[i].SetActive(false);
		}

	}


	public void Spawn()
	{
		if (m_activeDucks < m_ducks.Length)
		{
			m_ducks[m_activeDucks].transform.position = transform.position + Random.insideUnitSphere;
			m_ducks[m_activeDucks].SetActive(true);
			m_activeDucks++;
		}
	}
}
