using UnityEngine;

public class FluidFinder : MonoBehaviour
{
	public GameObject waterSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		FluidFrenzy.FluidSimulationManager.GetNeartestFluidLocation3D(transform.position, out Vector3 location);
		waterSource.transform.position = location;
		//transform.LookAt(location);

	}

	private void OnDrawGizmos()
	{
		//FluidFrenzy.FluidSimulationManager.GetNeartestFluidLocation(transform.position, out Vector3 location);
		//
		//FluidFrenzy.FluidSimulationManager.GetHeight(location, out Vector2 heightData);
		//Gizmos.color = Color.red;
		//location.y = heightData.x;
		//Gizmos.DrawCube(location, Vector3.one);
	}
}
