using UnityEngine;

public class BoneFollower : MonoBehaviour
{
	public Transform NeckBone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	// Helper function to clamp a quaternion's rotation around its parent
	public static Quaternion ClampRotation(Quaternion q, Vector3 bounds)
	{
		// Convert quaternion to local Euler angles
		Vector3 euler = q.eulerAngles;

		// Normalize angles to [-180, 180]
		euler.x = Mathf.Repeat(euler.x + 180f, 360f) - 180f;
		euler.y = Mathf.Repeat(euler.y + 180f, 360f) - 180f;
		euler.z = Mathf.Repeat(euler.z + 180f, 360f) - 180f;

		// Clamp each angle
		euler.x = Mathf.Clamp(euler.x, -bounds.x, bounds.x);
		euler.y = Mathf.Clamp(euler.y, -bounds.y, bounds.y);
		euler.z = Mathf.Clamp(euler.z, -bounds.z, bounds.z);

		// Return the clamped rotation
		return Quaternion.Euler(euler);
	}

	void LateUpdate()
	{
		// Get the parent's rotation
		Quaternion parentRotation = NeckBone.transform.parent.rotation;

		// Get camera's rotation
		Quaternion targetRotation = Camera.main.transform.rotation;

		// Optional: Convert camera rotation to local space relative to parent
		Quaternion localTargetRotation = Quaternion.Inverse(parentRotation) * targetRotation;

		// Convert to Euler for clamping
		Vector3 localEuler = localTargetRotation.eulerAngles;
		localEuler.x = Mathf.Repeat(localEuler.x + 180f, 360f) - 180f;
		localEuler.y = Mathf.Repeat(localEuler.y + 180f, 360f) - 180f;
		localEuler.z = Mathf.Repeat(localEuler.z + 180f, 360f) - 180f;

		// Clamp rotations: limit Y to ±90 degrees and Z to ±45 degrees
		localEuler.y = Mathf.Clamp(localEuler.y, -90f, 90f);
		localEuler.z = Mathf.Clamp(localEuler.z, -45f, 45f);

		// Reconstruct the clamped local rotation
		Quaternion clampedLocalRotation = Quaternion.Euler(localEuler);

		// Convert back to world space
		Quaternion finalRotation = parentRotation * clampedLocalRotation;

		// Apply the rotation
		NeckBone.transform.rotation = finalRotation;
	}
}
